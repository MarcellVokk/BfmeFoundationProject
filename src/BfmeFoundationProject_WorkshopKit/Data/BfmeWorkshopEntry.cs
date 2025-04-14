using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.WorkshopKit.Data
{
    public struct BfmeWorkshopEntry
    {
        public string Guid;
        public string Name;
        public string Version;
        public string Description;
        public string Changelog;
        public List<string> SocialLinks;
        public string Language;
        public string ArtworkUrl;
        public List<string> ScreenshotUrls;
        public string Author;
        public string Owner;
        public int Game;
        public int Type;
        public long CreationTime;
        public List<BfmeWorkshopFile> Files;
        public List<BfmeMap> Maps;
        public List<BfmeFaction> Factions;
        public List<string> Dependencies;

        public bool IsBaseGame() => Guid.StartsWith("original-") || Guid.StartsWith("exp-original-");
        public string GameName() => Game == 2 ? "RotWK" : $"BFME{Game + 1}";
        public string FullName() => $"{Name} ({Version})";
        public long PackageSize() => Files.Sum(x => x.Size);

        public BfmeWorkshopEntryHeader Header(bool includeVersion = false) => new BfmeWorkshopEntryHeader() { Guid = includeVersion ? $"{Guid}:{Version}" : Guid, Name = Name };
        public BfmeWorkshopEntryPreview Preview(BfmeWorkshopEntryMetadata metadata) => new BfmeWorkshopEntryPreview() { Guid = Guid, Name = Name, Version = Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, Size = PackageSize(), CreationTime = CreationTime, Metadata = metadata };
        
        public BfmeWorkshopEntry WithCreationTimeSetToNow() => new BfmeWorkshopEntry() { Guid = Guid, Name = Name, Version = Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, CreationTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds(), Files = Files, Maps = Maps, Factions = Factions, Dependencies = Dependencies };
        public BfmeWorkshopEntry WithThisAsBaseInfo(BfmeWorkshopEntry baseInfoSource, bool inheritVersion = false, bool inheritAuthor = false) => new BfmeWorkshopEntry() { Guid = baseInfoSource.Guid, Name = baseInfoSource.Name, Version = inheritVersion ? baseInfoSource.Version : Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = inheritAuthor ? baseInfoSource.Author : Author, Owner = baseInfoSource.Owner, Game = Game, Type = baseInfoSource.Type, CreationTime = CreationTime, Files = Files, Maps = Maps, Factions = Factions, Dependencies = Dependencies };
        public BfmeWorkshopEntry WithoutFiles() => new BfmeWorkshopEntry() { Guid = Guid, Name = Name, Version = Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, CreationTime = CreationTime, Files = [], Maps = Maps, Factions = Factions, Dependencies = Dependencies };
        public BfmeWorkshopEntry WithVersionBumped() => new BfmeWorkshopEntry() { Guid = Guid, Name = Name, Version = string.Join('.', Version.Split('.').SkipLast(1).Concat([int.TryParse(Version.Split('.').Last(), out int lv) ? (lv + 1).ToString() : Version.Split('.').Last()])), Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, CreationTime = CreationTime, Files = Files, Maps = Maps, Factions = Factions, Dependencies = Dependencies };

        public static async Task<BfmeWorkshopEntry> BaseGame(int game) => await BfmeWorkshopDownloadManager.Download($"original-{(game == 2 ? "RotWK" : $"BFME{game + 1}")}");
        public static async Task<BfmeWorkshopEntry> OfficialPatch(int game) => await BfmeWorkshopDownloadManager.Download($"official-{game}");

        public BfmeMap GetRandomMap(Dictionary<string, int>? weights = null)
        {
            if (weights == null)
            {
                return Maps[Random.Shared.Next(0, Maps.Count)];
            }
            else
            {
                List<BfmeMap> mapPool = Maps.Where(x => weights.ContainsKey(x.Id)).ToList();
                int poolSize = weights.Values.Sum();
                int randomNumber = Random.Shared.Next(0, poolSize) + 1;
                int accumulatedProbability = 0;

                foreach (var map in mapPool)
                {
                    accumulatedProbability += weights[map.Id];
                    if (randomNumber <= accumulatedProbability)
                        return map;
                }

                return mapPool.First();
            }
        }

        public string GetDestinationDirectory(Dictionary<int, (string gameLanguage, string gameDirectory, string dataDirectory)> virtualRegistry)
        {
            return Type switch
            {
                0 => virtualRegistry[Game].gameDirectory,
                1 => Path.Combine(string.Join('\\', virtualRegistry[Game].gameDirectory.Split(['\\', '/']).SkipLast(1)), "BFME Workshop", "Mods", $"{string.Join("", Name.Select(x => Path.GetInvalidPathChars().Contains(x) ? '_' : x))}-{Guid}"),
                2 => virtualRegistry[Game].gameDirectory,
                3 => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), virtualRegistry[Game].dataDirectory, "Maps", "Workshop"),
                4 => virtualRegistry[Game].gameDirectory,
                _ => virtualRegistry[Game].gameDirectory
            };
        }

        public static BfmeWorkshopEntry MakeNew(int game = 0, string author = "", string owner = "")
        {
            return new BfmeWorkshopEntry()
            {
                Guid = System.Guid.NewGuid().ToString(),
                Name = "",
                Version = "1.0.0",
                Description = "",
                Changelog = "",
                SocialLinks = [],
                Language = "",
                ArtworkUrl = "",
                ScreenshotUrls = [],
                Author = author,
                Owner = owner,
                Game = game,
                Type = 0,
                CreationTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds(),
                Files = [],
                Maps = [],
                Factions = [],
                Dependencies = [],
            };
        }
    }
}
