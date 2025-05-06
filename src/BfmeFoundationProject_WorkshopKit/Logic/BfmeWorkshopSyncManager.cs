using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using System.Diagnostics;
using System.Runtime.Versioning;
using BfmeFoundationProject.DirectXRuntime;
using System.Collections.Generic;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    [SupportedOSPlatform("windows")]
    public static class BfmeWorkshopSyncManager
    {
        private static FileSystemWatcher ActivePatchWatcher = new FileSystemWatcher();

        public static bool Syncing { get; private set; } = false;
        public static Action<BfmeWorkshopEntry>? OnSyncBegin;
        public static Action<int, string>? OnSyncUpdate;
        public static Action? OnSyncEnd;

        static BfmeWorkshopSyncManager()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Config")))
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Config"));

            ActivePatchWatcher.IncludeSubdirectories = true;
            ActivePatchWatcher.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Config");
            ActivePatchWatcher.NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
            ActivePatchWatcher.Filter = "*.*";
            ActivePatchWatcher.Created += (s, e) =>
            {
                if (Path.GetFileName(e.FullPath).StartsWith("active_patch_") && Path.GetFileName(e.FullPath).EndsWith(".syncing") && File.Exists(e.FullPath.Replace(".syncing", ".json")))
                    try { Syncing = true; OnSyncBegin?.Invoke(FileUtils.ReadJson(e.FullPath.Replace(".syncing", ".json"), new BfmeWorkshopEntry())); } catch { }
            };
            ActivePatchWatcher.Deleted += (s, e) =>
            {
                if (Path.GetFileName(e.FullPath).StartsWith("active_patch_") && Path.GetFileName(e.FullPath).EndsWith(".syncing") && Syncing)
                    try { Syncing = false; OnSyncEnd?.Invoke(); } catch { }
            };
            ActivePatchWatcher.Changed += (s, e) =>
            {
                if (Path.GetFileName(e.FullPath).StartsWith("active_patch_") && Path.GetFileName(e.FullPath).EndsWith(".syncing") && Syncing)
                    try { var data = ConfigUtils.ReadSyncProgress(e.FullPath); OnSyncUpdate?.Invoke(data.progress, data.status); } catch { }
            };
            ActivePatchWatcher.EnableRaisingEvents = true;
        }

        public static async Task Sync(BfmeWorkshopEntry entry, bool useFastFileCompare = true, List<string>? enhancements = null, Action<int, string>? OnProgressUpdate = null)
        {
            ActivePatchWatcher.EnableRaisingEvents = false;
            Syncing = true;
            OnSyncBegin?.Invoke(entry);
            OnProgressUpdate?.Invoke(0, "Preparing");
            OnSyncUpdate?.Invoke(0, "Preparing");

            try
            {
                if (!Directory.Exists(ConfigUtils.ConfigDirectory)) Directory.CreateDirectory(ConfigUtils.ConfigDirectory);

                var keybindsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{entry.GameName()}-{entry.Name}");
                var virtualRegistry = ConfigUtils.GetVirtualRegistry();
                var activeEnhancements = await BfmeWorkshopManager.GetActiveEnhancements(entry.Game);
                var scriptArtifacts = await BfmeWorkshopScriptManager.RunIfScripted(entry);
                var dependencies = new List<BfmeWorkshopEntry>();
                var files = new List<(BfmeWorkshopFile file, BfmeWorkshopEntry entry, bool isBaseFile, string destination)>();

                if (entry.Type == 0 || entry.Type == 1 || entry.Type == 4)
                {
                    // This is a patch, a mod or a snapshot, so sync it
                    await Task.Run(async () =>
                    {
                        // Update the active patch and signal that a sync has started
                        await ConfigUtils.SaveActivePatch(entry);
                        ConfigUtils.SaveSyncProgress(entry.Game, 0, "Preparing");

                        // Close the game to avoid any IO operation issues caused by files being locked by the game.
                        KillGame();

                        OnProgressUpdate?.Invoke(0, "Installing DX9 runtime");
                        OnSyncUpdate?.Invoke(0, "Installing DX9 runtime");
                        ConfigUtils.SaveSyncProgress(entry.Game, 0, "Installing DX9 runtime");

                        // Ensure DirectX9 is installed on the system.
                        await DirectXRuntimeManager.EnsureRuntimes();

                        // Ensure all default files and registry keys are created for the game.
                        BfmeRegistryManager.EnsureDefaults(entry.Game);

                        OnProgressUpdate?.Invoke(0, "Constructing dependency map");
                        OnSyncUpdate?.Invoke(0, "Constructing dependency map");
                        ConfigUtils.SaveSyncProgress(entry.Game, 0, "Constructing dependency map");

                        // If rotwk, add bfme2's enabled enhancements to active enhancements
                        if (entry.Game == 2)
                            activeEnhancements = activeEnhancements.Concat(await BfmeWorkshopManager.GetActiveEnhancements(1)).ToDictionary();

                        // If bfme2, add rotwk's enabled enhancements to active enhancements
                        if (entry.Game == 1 && BfmeRegistryManager.IsInstalled(2))
                            activeEnhancements = activeEnhancements.Concat(await BfmeWorkshopManager.GetActiveEnhancements(2)).ToDictionary();

                        // Download all explicit dependencies.
                        foreach (var dependency in entry.Dependencies.Where(x => x.Split(':')[0] != entry.Guid))
                            dependencies.Add(await GetDependency(dependency));

                        // Download all implicit dependencies defined by explicit dependencies.
                        while (dependencies.SelectMany(x => x.Dependencies).Any(x => x.Split(':')[0] != entry.Guid && !dependencies.Any(y => x.Split(':')[0] == y.Guid)))
                            foreach (var dependency in dependencies.SelectMany(x => x.Dependencies).Where(x => x.Split(':')[0] != entry.Guid && !dependencies.Any(y => x.Split(':')[0] == y.Guid)).ToList())
                                dependencies.Add(await GetDependency(dependency));

                        // Download all explicit enhancements.
                        foreach (var enhancement in (enhancements ?? new List<string>()).Where(x => x.Split(':')[0] != entry.Guid && !dependencies.Any(y => x.Split(':')[0] == y.Guid)))
                        {
                            var enhancementPackage = await GetDependency(enhancement);

                            if (enhancementPackage.Dependencies.Count > 0 && !enhancementPackage.Dependencies.Any(y => (y.Contains(':') ? $"{entry.Guid}:{entry.Version}" == y : entry.Guid == y) || dependencies.Any(x => y.Contains(':') ? $"{x.Guid}:{x.Version}" == y : x.Guid == y)))
                                throw new BfmeWorkshopEnhancementIncompatibleException($"'{enhancementPackage.FullName()}' is not compatible with '{entry.FullName()}'.");
                            else
                                dependencies.Add(enhancementPackage);
                        }

                        // Download all implicit dependencies defined by explicit enhancements.
                        while (dependencies.SelectMany(x => x.Dependencies).Any(x => x.Split(':')[0] != entry.Guid && !dependencies.Any(y => x.Split(':')[0] == y.Guid)))
                            foreach (var dependency in dependencies.SelectMany(x => x.Dependencies).Where(x => x.Split(':')[0] != entry.Guid && !dependencies.Any(y => x.Split(':')[0] == y.Guid)).ToList())
                                dependencies.Add(await GetDependency(dependency));

                        // If the curent patch is a mod, and a patch (whose game is not the same as the curent patches) was listed as a dependency, set the active patch of that game to it
                        if (entry.Type == 1 && dependencies.Any(x => x.Type == 0 && x.Game != entry.Game)) await ConfigUtils.SaveActivePatch(dependencies.First(x => x.Type == 0 && x.Game != entry.Game));

                        // Disable all enhancements that dont have the curent patch listed as a dependency, meaning they are not compatible (and if rotwk, disable all bfme2 enhancements).
                        // If there are explicit enhancements specified, disable all enhancements except those and the dependencies specified with the curent entry.
                        foreach (var enhancement in activeEnhancements.Values.Where(x => (x.Type == 2 && !x.Dependencies.Contains(entry.Guid) && !x.Dependencies.Contains($"{entry.Guid}:{entry.Version}")) || x.Game != entry.Game || enhancements != null))
                            ConfigUtils.DisableEnhancement(enhancement, activeEnhancements, virtualRegistry);

                        // Why? Because C#! (we do this to make sure the dict has the correct order if we re-add dependencies that have been removed in the above step. if we don't, and an ehnancement gets re-added, it retains it's index from where it was before it was removed)
                        activeEnhancements = activeEnhancements.ToDictionary();

                        // Enable all enhancements that are listed as dependencies (explicitly or implicitly) of the curent patch
                        foreach (var dependency in dependencies.Where(x => (x.Type == 2 || x.Type == 3) && (x.Dependencies.Contains(entry.Guid) || x.Dependencies.Contains($"{entry.Guid}:{entry.Version}") || dependencies.Any(y => x.Dependencies.Contains(y.Guid)) || dependencies.Any(y => x.Dependencies.Contains($"{y.Guid}:{y.Version}")))))
                            ConfigUtils.EnableEnhancement(dependency, activeEnhancements, virtualRegistry);

                        OnProgressUpdate?.Invoke(0, "Compiling file list");
                        OnSyncUpdate?.Invoke(0, "Compiling file list");
                        ConfigUtils.SaveSyncProgress(entry.Game, 0, "Compiling file list");

                        // Prepare the files needed for this configuration. Files added sooner have priority over files added later (files added sooner overwrite files added later with the same name)
                        // Files in L1 overwrite files in L2, L3 and L4, files in L2 overwrite files in L3 and L4, etc...

                        // L1 - Files from active enhancements
                        foreach (var enhancement in activeEnhancements.Values) AddFiles(enhancement, false, files, virtualRegistry);

                        // L2 - Files from the actual package
                        AddFiles(entry, entry.IsBaseGame(), files, virtualRegistry);

                        // L3 - Files from patches that are referenced as dependencies
                        foreach (var dependency in dependencies.Where(x => x.Type == 0)) AddFiles(dependency, false, files, virtualRegistry);

                        // L4 - Base game files (and if rotwk, bfme2's base game files. if bfme2, rotwk's base game files if it's installed)
                        if (!entry.IsBaseGame()) AddFiles(await BfmeWorkshopEntry.BaseGame(entry.Game), true, files, virtualRegistry);
                        if (entry.Game == 2) AddFiles(await BfmeWorkshopEntry.BaseGame(1), true, files, virtualRegistry);
                        if (entry.Game == 1 && BfmeRegistryManager.IsInstalled(2)) AddFiles(await BfmeWorkshopEntry.BaseGame(2), true, files, virtualRegistry);

                        // The files are now properly configured.

                        // Ensure all games referenced by this configuration are installed.
                        if (files.Select(x => x.entry.Game).Distinct().Any(x => virtualRegistry[x].gameDirectory == "" || !Directory.Exists(virtualRegistry[x].gameDirectory)))
                            throw new BfmeWorkshopGameNotInstalledException("One or more bfme games required by this mod/patch is not installed.");

                        // Ensure that none of the games referenced by this configuration are installed in a root directory (for example C: or D: as this could cause catastrophic damage)
                        if (files.Select(x => x.entry.Game).Distinct().Any(x => virtualRegistry[x].gameDirectory == "" || !Directory.Exists(virtualRegistry[x].gameDirectory) || virtualRegistry[x].gameDirectory.Replace("/", "\\").Trim('\\').Split('\\').Length <= 1))
                            throw new BfmeWorkshopGameNotInstalledException("One or more bfme games required by this mod/patch is installed in a root directory (for example C: or D:). The sync was aborted as continuing could cause catastrophic damage. Your files are safe, nothing was modified! It is highly recommended that you change your bfme install directory!");

                        // Begin the syncing process.
                        long totalFilesSize = files.Sum(x => x.file.Size);
                        long checkedFilesSize = 0;
                        int reportedProgress = 0;

                        foreach (var file in files.OrderBy(x => x.file.Size))
                        {
                            await EnsureFileExists(file.file, file.entry, file.isBaseFile, file.destination, useFastFileCompare, virtualRegistry,
                            OnDownloadProgress: (downloadProgress) =>
                            {
                                reportedProgress = (int)((checkedFilesSize + file.file.Size * (downloadProgress / 100d)) / totalFilesSize * 100d);
                                OnProgressUpdate?.Invoke(reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                                OnSyncUpdate?.Invoke(reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                                ConfigUtils.SaveSyncProgress(entry.Game, reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                            });

                            checkedFilesSize += file.file.Size;
                            if (reportedProgress != (int)(checkedFilesSize / (double)totalFilesSize * 100d))
                            {
                                reportedProgress = (int)(checkedFilesSize / (double)totalFilesSize * 100d);
                                OnProgressUpdate?.Invoke(reportedProgress, $"{file.file.Name}");
                                OnSyncUpdate?.Invoke(reportedProgress, $"{file.file.Name}");
                                ConfigUtils.SaveSyncProgress(entry.Game, reportedProgress, $"{file.file.Name}");
                            }
                        }

                        OnProgressUpdate?.Invoke(100, "Cleaning up");
                        OnSyncUpdate?.Invoke(100, "Cleaning up");
                        ConfigUtils.SaveSyncProgress(entry.Game, 100, "Cleaning up");

                        // Delete all files that are not required by the curent configuration
                        foreach (var game in files.Select(x => x.entry.Game).Distinct())
                        {
                            CleanUpFiles(entry, game, files, virtualRegistry, virtualRegistry[game].gameDirectory);
                            CleanUpFiles(entry, game, files, virtualRegistry, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), virtualRegistry[entry.Game].dataDirectory, "Maps", "Workshop"));
                            if (entry.Type == 1) CleanUpFiles(entry, game, files, virtualRegistry, Path.Combine(string.Join('\\', virtualRegistry[entry.Game].gameDirectory.Split(['\\', '/']).SkipLast(1)), "BFME Workshop", "Mods", $"{string.Join("", entry.Name.Select(x => Path.GetInvalidPathChars().Contains(x) ? '_' : x))}-{entry.Guid}"));
                        }

                        // If this is a scripted package, and we are missing requirements, throw.
                        // The reason we throw here and not at the beginning is so this sync function can be called to sync the dependencies of the package before presenting the scripted requirements, as some dependencies may be needed to complete some requirements.
                        if (scriptArtifacts.requirements.Any(x => !x.Value))
                            throw new BfmeWorkshopScriptMissingRequirementsException("This scripted package is missing one or more requirement.");

                        OnProgressUpdate?.Invoke(100, "Finishing up");
                        OnSyncUpdate?.Invoke(100, "Finishing up");
                        ConfigUtils.SaveSyncProgress(entry.Game, 100, "Finishing up");

                        // Update the mod.txt file to indicate which mod (if any) is active, so other applications can detect it, and launch the game with the -mod flag
                        if (entry.Type == 1)
                            ConfigUtils.SaveActiveMod(entry.Game, scriptArtifacts.files_directory != "" ? scriptArtifacts.files_directory : Path.Combine(string.Join('\\', virtualRegistry[entry.Game].gameDirectory.Split(['\\', '/']).SkipLast(1)), "BFME Workshop", "Mods", $"{string.Join("", entry.Name.Select(x => Path.GetInvalidPathChars().Contains(x) ? '_' : x))}-{entry.Guid}"));
                        else
                            ConfigUtils.SaveActiveMod(entry.Game, "");

                        OnProgressUpdate?.Invoke(100, "Copying custom keybinds");
                        OnSyncUpdate?.Invoke(100, "Copying custom keybinds");
                        ConfigUtils.SaveSyncProgress(entry.Game, 100, "Copying custom keybinds");

                        // Ensure the keybinds directory exists
                        if (!Directory.Exists(keybindsDirectory))
                            Directory.CreateDirectory(keybindsDirectory);

                        // Copy all the files from the keybinds folder to the game\lang folder folder
                        foreach (var langFileOverride in Directory.GetFiles(keybindsDirectory, "*.big"))
                            try { File.Copy(langFileOverride, Path.Combine(virtualRegistry[entry.Game].gameDirectory, "lang", Path.GetFileName(langFileOverride)), true); } catch { }

                        if (File.Exists(Path.Combine(keybindsDirectory, "_zzlotr.big")))
                            try { File.Copy(Path.Combine(keybindsDirectory, "_zzlotr.big"), Path.Combine(virtualRegistry[entry.Game].gameDirectory, "_zzlotr.big"), true); } catch { }
                    });
                }
                else if (entry.Type == 3)
                {
                    // This is a map mack, so we are doing a direct install of the files into the Maps folder
                    await Task.Run(async () =>
                    {
                        // Close the game to avoid any IO operation issues caused by files being locked by the game.
                        KillGame();

                        // Ensure all default files and registry keys are created for the game.
                        BfmeRegistryManager.EnsureDefaults(entry.Game);

                        OnProgressUpdate?.Invoke(0, "Compiling file list");
                        OnSyncUpdate?.Invoke(0, "Compiling file list");
                        ConfigUtils.SaveSyncProgress(entry.Game, 0, "Compiling file list");

                        // Prepare the files needed for this configuration. The only files needed are the ones in this map pack.
                        AddFiles(entry, false, files, virtualRegistry);

                        // The files are now properly configured.

                        // Ensure all games referenced by this configuration are installed.
                        if (files.Select(x => x.entry.Game).Distinct().Any(x => virtualRegistry[x].gameDirectory == "" || !Directory.Exists(virtualRegistry[x].gameDirectory)))
                            throw new BfmeWorkshopGameNotInstalledException("One or more bfme games required by this map pack is not installed.");

                        // Ensure that none of the games referenced by this configuration are installed in a root directory (for example C: or D: as this could cause catastrophic damage)
                        if (files.Select(x => x.entry.Game).Distinct().Any(x => virtualRegistry[x].gameDirectory == "" || !Directory.Exists(virtualRegistry[x].gameDirectory) || virtualRegistry[x].gameDirectory.Replace("/", "\\").Trim('\\').Split('\\').Length <= 1))
                            throw new BfmeWorkshopGameNotInstalledException("One or more bfme games required by this map pack is installed in a root directory (for example C: or D:). The sync was aborted as continuing could cause catastrophic damage. Your files are safe, nothing was modified! It is highly recommended that you change your bfme install directory!");

                        // Indicate that this map pack is now enabled
                        ConfigUtils.EnableEnhancement(entry, activeEnhancements, virtualRegistry);

                        // Begin the syncing process.
                        long totalFilesSize = files.Sum(x => x.file.Size);
                        long checkedFilesSize = 0;
                        int reportedProgress = 0;

                        foreach (var file in files.OrderBy(x => x.file.Size))
                        {
                            await EnsureFileExists(file.file, file.entry, file.isBaseFile, file.destination, useFastFileCompare, virtualRegistry,
                            OnDownloadProgress: (downloadProgress) =>
                            {
                                reportedProgress = (int)((checkedFilesSize + file.file.Size * (downloadProgress / 100d)) / totalFilesSize * 100d);
                                OnProgressUpdate?.Invoke(reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                                OnSyncUpdate?.Invoke(reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                                ConfigUtils.SaveSyncProgress(entry.Game, reportedProgress, $"{file.file.Name} ({downloadProgress}%)");
                            });

                            checkedFilesSize += file.file.Size;
                            if (reportedProgress != (int)(checkedFilesSize / (double)totalFilesSize * 100d))
                            {
                                reportedProgress = (int)(checkedFilesSize / (double)totalFilesSize * 100d);
                                OnProgressUpdate?.Invoke(reportedProgress, $"{file.file.Name}");
                                OnSyncUpdate?.Invoke(reportedProgress, $"{file.file.Name}");
                                ConfigUtils.SaveSyncProgress(entry.Game, reportedProgress, $"{file.file.Name}");
                            }
                        }

                        OnProgressUpdate?.Invoke(100, "Cleaning up");
                        OnSyncUpdate?.Invoke(100, "Cleaning up");
                        ConfigUtils.SaveSyncProgress(entry.Game, 100, "Cleaning up");

                        // Delete all files that are not required by the curent configuration
                        foreach (var game in files.Select(x => x.entry.Game).Distinct())
                            CleanUpFiles(entry, game, files, virtualRegistry, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), virtualRegistry[entry.Game].dataDirectory, "Maps", "Workshop"));
                    });
                }
                else
                {
                    // This is an enhancement, and can't be synced by itself
                    throw new BfmeWorkshopPackageNotSyncableException($"\"{entry.Name}\" is not a syncable package. It is only meant to be applied as an enhancement to other packages.");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Syncing = false;
                OnSyncEnd?.Invoke();
                await Task.Run(() => ConfigUtils.SaveSyncProgress(entry.Game, -1, ""));
                ActivePatchWatcher.EnableRaisingEvents = true;
            }
        }

        public static async Task<BfmeWorkshopEntry> CreateSnapshot(int game, bool cloneFiles = true)
        {
            var virtualRegistry = ConfigUtils.GetVirtualRegistry();
            string cacheDirectory = Path.Combine(virtualRegistry[game].gameDirectory.Split(['\\', '/'])[0], "BFME Workshop", "Cache");
            var baseGame = await BfmeWorkshopEntry.BaseGame(game);
            var files = new List<BfmeWorkshopFile>();

            if (!Directory.Exists(cacheDirectory)) Directory.CreateDirectory(cacheDirectory);

            await scanFolder(virtualRegistry[game].gameDirectory);
            async Task scanFolder(string folder)
            {
                // If not rotwk, and this folder is where rotwk is installed, skip this folder (rotwk can be installed inside bfme2's folder, and we don't want to be scanning)
                if (game == 1 && virtualRegistry[2].gameDirectory != "" && Directory.Exists(virtualRegistry[2].gameDirectory) && folder.ToLower().Trim('\\').Trim('/') == virtualRegistry[2].gameDirectory)
                    return;

                // Don't scan this folder, this is where cached files are.
                if (folder.Replace("/", "\\").Split('\\').Last() == "BFME Workshop")
                    return;

                // Check every file in this folder, and if it has been modified compared to the base game, add it to the modified files list
                foreach (var file in Directory.GetFiles(folder))
                {
                    string name = file.ToLower().Replace(virtualRegistry[game].gameDirectory, "").Trim('\\').Trim('/');
                    var md5 = await FileUtils.Hash(file);

                    // If a file with the same name and checksum already exists in the configuration, skip it, we already have it.
                    if (baseGame.Files.Any(x => x.Name.ToLower() == name && x.Md5 == md5))
                        continue;

                    // Clone file if requested
                    if (cloneFiles)
                        File.Copy(file, Path.Combine(cacheDirectory, $"{md5}.pfcache"), true);

                    files.Add(new BfmeWorkshopFile() { Guid = md5, Name = name, Language = "ALL", Md5 = md5, Url = cloneFiles ? Path.Combine(cacheDirectory, $"{md5}.pfcache") : file });
                }

                // Call this method recursively for all subfolders
                foreach (var subFolder in Directory.GetDirectories(folder))
                    await scanFolder(subFolder);
            }

            return new BfmeWorkshopEntry() { Guid = $"snapshot-{(game == 2 ? "RotWK" : $"BFME{game + 1}")}-{Guid.NewGuid()}", Name = $"{(game == 2 ? "RotWK" : $"BFME{game + 1}")} Snapshot", Version = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), Description = $"This snapshot was created on {DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}.", ArtworkUrl = $"{BfmeWorkshopManager.WorkshopFilesHost}/snapshot-artwork.png", Author = "Me", Owner = "", Game = game, Type = 4, CreationTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds(), Files = files, Dependencies = new List<string>() };
        }

        private static async Task<BfmeWorkshopEntry> GetDependency(string entryGuid)
        {
            try
            {
                return await BfmeWorkshopDownloadManager.Download(entryGuid);
            }
            catch
            {
                return await BfmeWorkshopLibraryManager.Get(entryGuid);
            }
        }

        private static void AddFiles(BfmeWorkshopEntry entry, bool isBaseEntry, List<(BfmeWorkshopFile file, BfmeWorkshopEntry entry, bool isBaseFile, string destination)> files, Dictionary<int, (string gameLanguage, string gameDirectory, string dataDirectory)> virtualRegistry)
        {
            var filesFromEntry = new List<(BfmeWorkshopFile file, BfmeWorkshopEntry entry, bool isBaseFile, string destination)>();

            // Add the files from this entry to the curent configuration
            foreach (var file in entry.Files)
            {
                var destination = Path.Combine(entry.GetDestinationDirectory(virtualRegistry), file.Name).ToLower().TrimStart('\\').TrimStart('/');

                // Exclude files that are to be excluded unconditionaly because they are in ConfigUtils.IgnoredGameFiles
                if (ConfigUtils.IgnoredGameFiles.Contains(Path.GetFileName(file.Name).ToLower()))
                    continue;

                // Exclude files that are not language neutral and also do not match the curent language of the game
                if (file.Language != "ALL" && file.Language != "" && !file.Language.Split(' ').Contains(virtualRegistry[entry.Game].gameLanguage))
                    continue;

                // Exclude files that have already been added by a previous entry (meaning this file was overwritten by an entry that has higher priority, and had a file with the same exact destination path)
                if (files.Any(x => x.destination == destination))
                    continue;

                files.Add((file: file, entry: entry, isBaseFile: isBaseEntry, destination: destination));
            }
        }

        private static async Task EnsureFileExists(BfmeWorkshopFile file, BfmeWorkshopEntry entry, bool isBaseFile, string destination, bool useFastFileCompare, Dictionary<int, (string gameLanguage, string gameDirectory, string dataDirectory)> virtualRegistry, Action<int> OnDownloadProgress)
        {
            string cacheDirectory = Path.Combine(string.Join('\\', virtualRegistry[entry.Game].gameDirectory.Split(['\\', '/']).SkipLast(1)), "BFME Workshop", "Cache");
            string secondaryCacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Cache");
            bool shouldUseFastFileCompare = useFastFileCompare && file.Size > 0 && Path.GetExtension(file.Name) != ".dat" && Path.GetExtension(file.Name) != ".exe";
            bool shouldCache = isBaseFile ? (Path.GetExtension(file.Name) == ".dat" || Path.GetExtension(file.Name) == ".exe") : true;

            if (Path.GetExtension(file.Name) == ".wps")
                return;

            if (!Directory.Exists(cacheDirectory)) Directory.CreateDirectory(cacheDirectory);
            if (!Directory.Exists(Path.GetDirectoryName(destination))) Directory.CreateDirectory(Path.GetDirectoryName(destination) ?? "");

            // If the file is from a local source, verify it's copied correctly into the destination directory
            if (!file.Url.StartsWith("http"))
            {
                if (!File.Exists(file.Url))
                    throw new BfmeWorkshopFileMissingException($"The file {file.Url} is missing.");

                if (file.Url.ToLower().TrimStart('\\').TrimStart('/') != destination.ToLower().TrimStart('\\').TrimStart('/'))
                {
                    if (File.Exists(destination))
                    {
                        if (shouldUseFastFileCompare)
                        {
                            long size = FileUtils.Size(destination);
                            if (size == file.Size)
                                return;
                        }
                        else
                        {
                            string hash = await FileUtils.Hash(destination);
                            if (hash == file.Md5)
                                return;
                        }
                    }

                    File.Copy(file.Url, destination, true);
                }

                return;
            }

            // If the file exists in the destination directory and the file is the same as requested, do nothing
            if (File.Exists(destination))
            {
                if (shouldUseFastFileCompare)
                {
                    long size = FileUtils.Size(destination);
                    if (size == file.Size)
                    {
                        if (!shouldCache && entry.Type != 1 && !File.Exists(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache")))
                            await Task.Run(() => File.Copy(destination, Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), true));
                        else if (!shouldCache && entry.Type != 1 && FileUtils.Size(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache")) != size)
                            await Task.Run(() => File.Copy(destination, Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), true));
                        return;
                    }
                }
                else
                {
                    string hash = await FileUtils.Hash(destination);
                    if (hash == file.Md5)
                    {
                        if (!shouldCache && entry.Type != 1 && !File.Exists(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache")))
                            await Task.Run(() => File.Copy(destination, Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), true));
                        else if (!shouldCache && entry.Type != 1 && await FileUtils.Hash(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache")) != hash)
                            await Task.Run(() => File.Copy(destination, Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), true));
                        return;
                    }
                }
            }

            // If this is not a mod, try to load the file from the local cache
            if (entry.Type != 1)
            {
                if (File.Exists(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache")))
                {
                    if (shouldUseFastFileCompare)
                    {
                        long size = FileUtils.Size(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"));
                        if (size == file.Size)
                        {
                            await Task.Run(() => File.Copy(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), destination, true));
                            return;
                        }
                    }
                    else
                    {
                        string hash = await FileUtils.Hash(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"));
                        if (hash == file.Md5)
                        {
                            await Task.Run(() => File.Copy(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), destination, true));
                            return;
                        }
                    }
                }

                if (File.Exists(Path.Combine(secondaryCacheDirectory, $"{file.Guid}.pfcache")))
                {
                    if (shouldUseFastFileCompare)
                    {
                        long size = FileUtils.Size(Path.Combine(secondaryCacheDirectory, $"{file.Guid}.pfcache"));
                        if (size == file.Size)
                        {
                            await Task.Run(() => File.Copy(Path.Combine(secondaryCacheDirectory, $"{file.Guid}.pfcache"), destination, true));
                            return;
                        }
                    }
                    else
                    {
                        string hash = await FileUtils.Hash(Path.Combine(secondaryCacheDirectory, $"{file.Guid}.pfcache"));
                        if (hash == file.Md5)
                        {
                            await Task.Run(() => File.Copy(Path.Combine(secondaryCacheDirectory, $"{file.Guid}.pfcache"), destination, true));
                            return;
                        }
                    }
                }
            }

            // If the file was not found in neither the destination directory nor in the local cache, download the file to either the cache or directly to the destination
            await HttpMarshal.GetFile(
            url: file.Url,
            localPath: (shouldCache && entry.Type != 1) ? Path.Combine(cacheDirectory, $"{file.Guid}.pfcache") : destination,
            headers: [],
            OnProgressUpdate: OnDownloadProgress);

            // If the file was downloaded into cache, copy the file from the cache directory to the destination
            if (shouldCache && entry.Type != 1)
                await Task.Run(() => File.Copy(Path.Combine(cacheDirectory, $"{file.Guid}.pfcache"), destination, true));
        }

        private static void CleanUpFiles(BfmeWorkshopEntry entry, int game, List<(BfmeWorkshopFile file, BfmeWorkshopEntry entry, bool isBaseFile, string destination)> files, Dictionary<int, (string gameLanguage, string gameDirectory, string dataDirectory)> virtualRegistry, string root)
        {
            // If this directory doesn't exist, return.
            if (!Directory.Exists(root))
                return;

            // If not rotwk, and this folder is where rotwk is installed, skip this folder (rotwk can be installed inside bfme2's folder, and we don't want to accidentaly delete it)
            if (game != 2 && virtualRegistry[2].gameDirectory != "" && Directory.Exists(virtualRegistry[2].gameDirectory) && root.ToLower().Trim('\\').Trim('/') == virtualRegistry[2].gameDirectory)
                return;

            // Don't delete this folder, as this is where we put cached files.
            if (root.Replace("/", "\\").Split('\\').Last() == "BFME Workshop")
                return;

            // Delete all files in this directory that are not in IgnoredGameFiles, and are not found in the curent file configuration
            foreach (string file in Directory.GetFiles(root))
                if (!ConfigUtils.IgnoredGameFiles.Contains(Path.GetFileName(file).ToLower()) && !files.Any(x => file.ToLower() == x.destination))
                    File.Delete(file);

            // Call this method recursively on all subfolders in this directory
            foreach (string subFolder in Directory.GetDirectories(root))
                CleanUpFiles(entry, game, files, virtualRegistry, subFolder);

            // If this directory is fully empty, delete it
            if (Directory.GetFiles(root).Length == 0 && Directory.GetDirectories(root).Length == 0)
                Directory.Delete(root, true);
        }

        private static void KillGame()
        {
            foreach (Process p in Process.GetProcessesByName("game.dat"))
                try { p.Kill(); } catch { }
            foreach (Process p in Process.GetProcessesByName("lotrbfme"))
                try { p.Kill(); } catch { }
            foreach (Process p in Process.GetProcessesByName("lotrbfme2"))
                try { p.Kill(); } catch { }
            foreach (Process p in Process.GetProcessesByName("lotrbfme2ep1"))
                try { p.Kill(); } catch { }
        }
    }
}