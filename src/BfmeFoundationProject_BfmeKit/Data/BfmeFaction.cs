namespace BfmeFoundationProject.BfmeKit.Data
{
    public struct BfmeFaction
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string BigIcon { get; set; }
        public string SmallIcon { get; set; }

        public BfmeFaction(string name, int id, string bigIcon, string smallIcon)
        {
            Name = name;
            Id = id;
            BigIcon = bigIcon;
            SmallIcon = smallIcon;
        }

        public static List<BfmeFaction> StandardBfme1Factions()
        {
            return new List<BfmeFaction>()
            {
                new BfmeFaction("Rohan", 3, "STANDARD:rohan.png", "STANDARD:rohan.png"),
                new BfmeFaction("Gondor", 4, "STANDARD:gondor.png", "STANDARD:gondor.png"),
                new BfmeFaction("Isengard", 5, "STANDARD:isengard.png", "STANDARD:isengard.png"),
                new BfmeFaction("Mordor", 6, "STANDARD:mordor.png", "STANDARD:mordor.png")
            };
        }

        public static List<BfmeFaction> StandardBfme2Factions()
        {
            return new List<BfmeFaction>()
            {
                new BfmeFaction("Men", 3, "STANDARD:men.png", "STANDARD:men.png"),
                new BfmeFaction("Elves", 5, "STANDARD:elves.png", "STANDARD:elves.png"),
                new BfmeFaction("Dwarves", 6, "STANDARD:dwarves.png", "STANDARD:dwarves.png"),
                new BfmeFaction("Isengard", 7, "STANDARD:isengard.png", "STANDARD:isengard.png"),
                new BfmeFaction("Mordor", 8, "STANDARD:mordor.png", "STANDARD:mordor.png"),
                new BfmeFaction("Goblins", 9, "STANDARD:goblins.png", "STANDARD:goblins.png")
            };
        }

        public static List<BfmeFaction> StandardRotWkFactions()
        {
            return new List<BfmeFaction>()
            {
                new BfmeFaction("Men", 3, "STANDARD:men.png", "STANDARD:men.png"),
                new BfmeFaction("Elves", 5, "STANDARD:elves.png", "STANDARD:elves.png"),
                new BfmeFaction("Dwarves", 6, "STANDARD:dwarves.png", "STANDARD:dwarves.png"),
                new BfmeFaction("Isengard", 7, "STANDARD:isengard.png", "STANDARD:isengard.png"),
                new BfmeFaction("Mordor", 8, "STANDARD:mordor.png", "STANDARD:mordor.png"),
                new BfmeFaction("Goblins", 9, "STANDARD:goblins.png", "STANDARD:goblins.png"),
                new BfmeFaction("Angmar", 10, "STANDARD:angmar.png", "STANDARD:angmar.png")
            };
        }
    }
}
