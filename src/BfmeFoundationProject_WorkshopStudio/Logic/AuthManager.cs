using BfmeFoundationProject.WorkshopStudio.Pages;
using BfmeFoundationProject.WorkshopStudio.Pages.SubPages;
using BfmeFoundationProject.Shared.API;
using BfmeFoundationProject.Shared.Data.Accounts;
using BfmeFoundationProject.Shared.Utils;
using BfmeFoundationProject.WorkshopKit.Logic;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace BfmeFoundationProject.WorkshopStudio.Logic
{
    public static class AuthManager
    {
        public static bool IsAuthenticated = false;
        public static PlayerAccount CurentUser = PlayerAccount.Empty;

        public static async void Authenticate(string email, string password, Action<string> OnStatusUpdate, Action<string> OnFailed)
        {
            OnStatusUpdate.Invoke("AUTHENTICATING");
            string passwordHash = await Task.Run(() => SecurityUtils.GetStringSha256Hash(SecurityUtils.GetStringSha256Hash(password)));

            Tuple<string, PlayerAccount?> authResponse = await AuthApi.Authenticate(email, passwordHash);

            if (authResponse.Item1 != "" || authResponse.Item2 == null)
            {
                OnFailed(authResponse.Item1);
            }
            else
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Settings")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Settings"));

                CurentUser = authResponse.Item2.Value;
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Settings", "arena_loginCredentials.json"), JsonConvert.SerializeObject(new string[] { email, password }));
                IsAuthenticated = true;

                OnStatusUpdate.Invoke("LOADING WORKSHOP");
                await WorkshopPage.Reload();

                OnStatusUpdate.Invoke("LOADING UI");
                await Task.Delay(100);

                MainWindow.Instance.content.Child = new MissionControlPage();
            }
        }

        public static void Logout()
        {
            IsAuthenticated = false;
            CurentUser = PlayerAccount.Empty;
            MainWindow.Instance.content.Child = new AuthenticatePage();
        }
    }
}
