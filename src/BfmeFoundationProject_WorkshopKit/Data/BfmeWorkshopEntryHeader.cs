namespace BfmeFoundationProject.WorkshopKit.Data
{
    public struct BfmeWorkshopEntryHeader
    {
        public string Guid;
        public string Name;

        public BfmeWorkshopEntry WorkshopEntry() => new BfmeWorkshopEntry() { Guid = Guid, Name = Name, Version = "", Description = "", Changelog = "", SocialLinks = [], Language = "", ArtworkUrl = "", ScreenshotUrls = [], Author = "", Owner = "", Game = 0, Type = 0, CreationTime = 0, Files = [], Maps = [], Factions = [], Dependencies = [] };
    }
}
