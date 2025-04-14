using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopDownloadManager
    {
        public static async Task<BfmeWorkshopEntry> Download(string entryGuid)
        {
            return await HttpUtils.GetJson<BfmeWorkshopEntry>(BfmeWorkshopAuthInfo.Unauthenticated, "workshop/download", new Dictionary<string, string>()
            {
                { "guid", entryGuid }
            });
        }
    }
}
