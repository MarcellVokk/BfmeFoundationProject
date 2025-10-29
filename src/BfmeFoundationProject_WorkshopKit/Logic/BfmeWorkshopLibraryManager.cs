using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    [SupportedOSPlatform("windows")]
    public static class BfmeWorkshopLibraryManager
    {
        public static async Task<List<BfmeWorkshopEntryPreview>> Query(string keyword = "", int game = -1, int type = -1, int page = 0)
        {
            return await Task.Run(async () =>
            {
                List<BfmeWorkshopEntryPreview> libraryEntries = new List<BfmeWorkshopEntryPreview>();

                if (FileUtils.ReadText(Path.Combine(ConfigUtils.ConfigDirectory, "library_version.flag"), "") != "2")
                {
                    if (Directory.Exists(ConfigUtils.LibraryDirectory))
                        Directory.Delete(ConfigUtils.LibraryDirectory, true);

                    FileUtils.WriteText(Path.Combine(ConfigUtils.ConfigDirectory, "library_version.flag"), "2");
                }

                if (!Directory.Exists(ConfigUtils.LibraryDirectory))
                    Directory.CreateDirectory(ConfigUtils.LibraryDirectory);

                foreach (var file in Directory.GetFiles(ConfigUtils.LibraryDirectory).OrderBy(x => new FileInfo(x).CreationTime))
                    try { libraryEntries.Add(FileUtils.ReadJson(file, new BfmeWorkshopEntry()).Preview(new BfmeWorkshopEntryMetadata())); } catch { }

                for (int i = 0; i < 3; i++)
                {
                    if (!libraryEntries.Any(x => x.Guid == $"original-{(i < 2 ? $"BFME{i + 1}" : "RotWK")}"))
                    {
                        AddOrUpdate(await BfmeWorkshopDownloadManager.Download($"original-{(i < 2 ? $"BFME{i + 1}" : "RotWK")}"));
                        libraryEntries.Add(FileUtils.ReadJson(Path.Combine(ConfigUtils.LibraryDirectory, $"original-{(i < 2 ? $"BFME{i + 1}" : "RotWK")}.json"), new BfmeWorkshopEntry()).Preview(new BfmeWorkshopEntryMetadata()));
                    }
                    if (!libraryEntries.Any(x => x.Guid == $"official-{i}"))
                    {
                        AddOrUpdate(await BfmeWorkshopDownloadManager.Download($"official-{i}"));
                        libraryEntries.Add(FileUtils.ReadJson(Path.Combine(ConfigUtils.LibraryDirectory, $"official-{i}.json"), new BfmeWorkshopEntry()).Preview(new BfmeWorkshopEntryMetadata()));
                    }
                }

                libraryEntries = libraryEntries.Where(x => x.Guid != null && x.Name != null && x.Description != null).OrderByDescending(x => x.Guid.StartsWith("official-")).OrderByDescending(x => x.Guid.StartsWith("original-")).OrderByDescending(x => x.Guid.StartsWith("exp-original-")).Where(x => (keyword.Length > 0 ? (x.Name.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower())) : true) && (game != -1 ? x.Game == game : true) && (type != -1 ? (type == -2 ? (x.Type == 0 || x.Type == 1) : (type == -3 ? (x.Type == 2 || x.Type == 3) : x.Type == type)) : true)).ToList();

                if (page == -1)
                    return libraryEntries;

                if (libraryEntries.Count - page * 25 > 0)
                    return libraryEntries.GetRange(page * 25, Math.Min(25, libraryEntries.Count - page * 25)).Select(x => x.WithEmptyMetadata()).ToList();
                else
                    return new List<BfmeWorkshopEntryPreview>();
            });
        }

        public static async Task<BfmeWorkshopEntry> Get(string entryGuid)
        {
            try
            {
                if (File.Exists(Path.Combine(ConfigUtils.LibraryDirectory, $"{entryGuid}.json")))
                    return await Task.Run(() => FileUtils.ReadJson(Path.Combine(ConfigUtils.LibraryDirectory, $"{entryGuid}.json"), new BfmeWorkshopEntry()));
            }
            catch { }

            throw new BfmeWorkshopEntryNotFoundException($"The package with guid {entryGuid} was not found in the library!");
        }

        public static void AddOrUpdate(BfmeWorkshopEntry entry)
        {
            string keybindsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{entry.GameName()}-{entry.Name}");

            if (!Directory.Exists(ConfigUtils.LibraryDirectory)) Directory.CreateDirectory(ConfigUtils.LibraryDirectory);
            if (entry.Type <= 1 && !Directory.Exists(keybindsDirectory)) Directory.CreateDirectory(keybindsDirectory);

            FileUtils.WriteJson(Path.Combine(ConfigUtils.LibraryDirectory, $"{entry.Guid}.json"), entry);
        }

        public static void Remove(string entryGuid)
        {
            try
            {
                if (File.Exists(Path.Combine(ConfigUtils.LibraryDirectory, $"{entryGuid}.json")))
                    File.Delete(Path.Combine(ConfigUtils.LibraryDirectory, $"{entryGuid}.json"));
            }
            catch { }
        }

        public static bool IsInLibrary(string entryGuid)
        {
            return File.Exists(Path.Combine(ConfigUtils.LibraryDirectory, $"{entryGuid}.json"));
        }
    }
}
