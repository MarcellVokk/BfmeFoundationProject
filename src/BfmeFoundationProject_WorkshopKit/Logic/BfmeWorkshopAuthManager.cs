using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopAuthManager
    {
        public static async Task<BfmeWorkshopAuthInfo> Authenticate(string email, string password)
        {
            string passwordHash = await Task.Run(() => GetStringSha256Hash(GetStringSha256Hash(password)));
            string[] authResult = await HttpUtils.GetJson<string[]>(BfmeWorkshopAuthInfo.Unauthenticated, "auth/login", new Dictionary<string, string>() { { "email", email }, { "password", passwordHash } }) ?? new string[3];

            if (authResult[0] == "user_not_found")
                throw new Exception("user_not_found");
            else if (authResult[0] == "wrong_password")
                throw new Exception("wrong_password");
            else if (authResult[0] == "error")
                throw new Exception("error");
            else if (authResult[0].StartsWith("suspended"))
                throw new Exception(authResult[0]);

            return JsonConvert.DeserializeObject<BfmeWorkshopAuthInfo>(authResult[2]);
        }

        private static string GetStringSha256Hash(string text)
        {
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
