using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using Newtonsoft.Json;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopAdminManager
    {
        public static async Task<BfmeWorkshopEntry> Publish(BfmeWorkshopAuthInfo authInfo, BfmeWorkshopEntry entry)
        {
            string response = await HttpUtils.SetString(authInfo, "workshop", JsonConvert.SerializeObject(entry));
            if (!response.StartsWith("{"))
                throw new Exception(response);

            return JsonConvert.DeserializeObject<BfmeWorkshopEntry>(response);
        }

        public static async Task Transfer(BfmeWorkshopAuthInfo authInfo, string entryGuid, string newOwner)
        {
            string response = await HttpUtils.GetString(authInfo, "workshop/transfer", new Dictionary<string, string>()
            {
                { "guid", entryGuid },
                { "newOwner", newOwner },
            });

            if (response != JsonConvert.SerializeObject(entryGuid))
                throw new Exception(response);
        }

        public static async Task Unlist(BfmeWorkshopAuthInfo authInfo, string entryGuid)
        {
            string response = await HttpUtils.Delete(authInfo, "workshop", id: entryGuid);
            if (response != "")
                throw new Exception(response);
        }

        public static async Task UploadFile(BfmeWorkshopAuthInfo authInfo, string fileName, string source, Action<int>? OnProgressUpdate = null)
        {
            await HttpUtils.Upload(authInfo, source, id: fileName, OnProgressUpdate: OnProgressUpdate);
        }
    }
}
