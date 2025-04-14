namespace BfmeFoundationProject.WorkshopKit.Data
{
    public struct BfmeWorkshopAuthInfo
    {
        public string Uuid = "";
        public string Password = "";

        public BfmeWorkshopAuthInfo(string accountUuid, string accountPassword)
        {
            Uuid = accountUuid;
            Password = accountPassword;
        }

        public static BfmeWorkshopAuthInfo Unauthenticated => new BfmeWorkshopAuthInfo("unauthenticated", "");
    }
}
