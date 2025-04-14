namespace BfmeFoundationProject.WorkshopKit.Data
{
    public struct BfmeWorkshopEntryMetadata
    {
        public int Downloads;
        public List<string> Versions;

        public BfmeWorkshopEntryMetadata()
        {
            Downloads = 0;
            Versions = new List<string>();
        }
    }
}
