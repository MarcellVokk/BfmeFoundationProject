namespace BfmeFoundationProject.BfmeKit.Data
{
    public struct BfmeSpot
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Team { get; set; }
        public int Index {  get; set; }

        public BfmeSpot(float x, float y, int team, int index)
        {
            X = x;
            Y = y;
            Team = team;
            Index = index;
        }
    }
}
