using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopQueryManager
    {
        public static async Task<List<BfmeWorkshopEntryPreview>> Query(string keyword = "", int game = -1, int type = -1, int sortMode = 0, int page = 0)
        {
            return await HttpUtils.GetJson<List<BfmeWorkshopEntryPreview>>(BfmeWorkshopAuthInfo.Unauthenticated, "workshop/query", new Dictionary<string, string>()
            {
                { "keyword", keyword },
                { "game", game.ToString() },
                { "type", type.ToString() },
                { "sortMode", sortMode.ToString() },
                { "page", page.ToString() }
            }) ?? new List<BfmeWorkshopEntryPreview>();
        }

        public static async Task<List<BfmeWorkshopEntryPreview>> Query(string ownerUuid)
        {
            return await HttpUtils.GetJson<List<BfmeWorkshopEntryPreview>>(BfmeWorkshopAuthInfo.Unauthenticated, "workshop/query", new Dictionary<string, string>()
            {
                { "ownerUuid", ownerUuid }
            }) ?? new List<BfmeWorkshopEntryPreview>().OrderBy(x => x.Name).OrderByDescending(x => x.Guid.StartsWith("official-")).OrderByDescending(x => x.Guid.StartsWith("original-")).OrderByDescending(x => x.Guid.StartsWith("exp-original-")).ToList();
        }

        public static async Task<List<BfmeWorkshopEntryPreview>> QueryAll(BfmeWorkshopAuthInfo authInfo)
        {
            return (await HttpUtils.GetJson<List<BfmeWorkshopEntryPreview>>(authInfo, "workshop/query", new Dictionary<string, string>()
            {
                { "ownerUuid", "*" }
            }) ?? new List<BfmeWorkshopEntryPreview>()).OrderBy(x => x.Name).OrderByDescending(x => x.Guid.StartsWith("official-")).OrderByDescending(x => x.Guid.StartsWith("original-")).OrderByDescending(x => x.Guid.StartsWith("exp-original-")).ToList();
        }

        public static async Task<BfmeWorkshopEntryPreview> Get(string entryGuid)
        {
            return await HttpUtils.GetJson<BfmeWorkshopEntryPreview>(BfmeWorkshopAuthInfo.Unauthenticated, "workshop", new Dictionary<string, string>() { { "guid", entryGuid } });
        }
    }
}
