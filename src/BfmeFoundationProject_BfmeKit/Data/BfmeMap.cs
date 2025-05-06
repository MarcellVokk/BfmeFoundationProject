namespace BfmeFoundationProject.BfmeKit.Data
{
    public struct BfmeMap
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Game { get; set; }
        public string Preview { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public List<BfmeSpot> Spots { get; set; }

        public BfmeMap(string id, string name, int game, string preview, float width, float height, List<BfmeSpot> spots)
        {
            Id = id;
            Name = name;
            Game = game;
            Preview = preview;
            Width = width;
            Height = height;
            Spots = spots;
        }

        public void RandomizeSpots()
        {
            Spots = Spots.OrderBy(x => Random.Shared.Next()).ToList();
        }
    }
}
