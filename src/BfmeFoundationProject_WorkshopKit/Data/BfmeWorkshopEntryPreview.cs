namespace BfmeFoundationProject.WorkshopKit.Data
{
    public struct BfmeWorkshopEntryPreview
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
        public long Size;
        public long CreationTime;
        public BfmeWorkshopEntryMetadata Metadata;

        public string GameName() => Game == -1 ? "Unknown" : (Game == 2 ? "RotWK" : $"BFME{Game + 1}");
        public string FullName() => Version == "" ? Name : $"{Name} ({Version})";

        public BfmeWorkshopEntryPreview WithEmptyMetadata() => new BfmeWorkshopEntryPreview() { Guid = Guid, Name = Name, Version = Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, Size = Size, CreationTime = CreationTime, Metadata = new BfmeWorkshopEntryMetadata() };

        public BfmeWorkshopEntryHeader Header() => new BfmeWorkshopEntryHeader() { Guid = Guid, Name = Name };
        public BfmeWorkshopEntry WorkshopEntry() => new BfmeWorkshopEntry() { Guid = Guid, Name = Name, Version = Version, Description = Description, Changelog = Changelog, SocialLinks = SocialLinks, Language = Language, ArtworkUrl = ArtworkUrl, ScreenshotUrls = ScreenshotUrls, Author = Author, Owner = Owner, Game = Game, Type = Type, CreationTime = CreationTime, Files = [], Maps = [], Factions = [], Dependencies = [] };
    }
}
