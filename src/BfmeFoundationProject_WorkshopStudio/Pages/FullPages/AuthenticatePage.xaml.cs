using BfmeFoundationProject.WorkshopStudio.Logic;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BfmeFoundationProject.WorkshopStudio.Pages
{
    /// <summary>
    /// Interaction logic for AuthenticatePage.xaml
    /// </summary>
    public partial class AuthenticatePage : UserControl
    {
        private static bool TryAutoLogin = true;

        public AuthenticatePage()
        {
            InitializeComponent();

            if (TryAutoLogin)
            {
                TryAutoLogin = false;

                if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Settings", "arena_loginCredentials.json")))
                {
                    string[] credentials = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Settings", "arena_loginCredentials.json"))) ?? new string[] { "", "" };
                    textbox_email.Text = credentials[0];
                    textbox_password.Password = credentials[1];
                    Authenticate();
                }
            }
        }

        private void button_connect_Click(object sender, RoutedEventArgs e)
        {
            Authenticate();
        }

        private async void Authenticate()
        {
            if (!button_connect.IsHitTestVisible)
                return;

            error.Text = "";

            button_connect.Visibility = Visibility.Collapsed;
            button_connect_loading.IsLoading = true;
            button_connect_loading.Visibility = Visibility.Visible;

            loginInfo.Visibility = Visibility.Collapsed;
            status.Visibility = Visibility.Visible;

            status.Text = "CHECKING FOR UPDATES";

            if (App.IsReleaseBuild && await UpdateManager.CheckForUpdates())
            {
                UpdatePage updatePage = new UpdatePage();
                MainWindow.Instance.content.Child = updatePage;

                await UpdateManager.Update("main", (updateProgress) => Dispatcher.Invoke(() => updatePage.UpdateProgress = updateProgress));

                string applicationPath = Process.GetCurrentProcess().MainModule!.FileName ?? "";
                Process.Start(applicationPath);

                Application.Current.Shutdown();
            }
            else
            {
                AuthManager.Authenticate(textbox_email.Text, textbox_password.Password, HandleAuthenticateStatusUpdate, HandleAuthenticateFailed);
            }
        }

        private void HandleAuthenticateStatusUpdate(string newStatus)
        {
            status.Text = newStatus;
        }

        private void HandleAuthenticateFailed(string reason)
        {
            if (reason == "error")
                error.Text = "AN UNKNOWN ERROR OCCURED";
            else if (reason == "user_not_found")
                error.Text = "THIS ACCOUNT DOESN'T EXIST";
            else if (reason == "wrong_password")
                error.Text = "YOU ENTERED THE WRONG PASSWORD";
            else if (reason == "access_denied")
                error.Text = "THIS ACCOUNT DOESN'T HAVE PERMISSIONS TO ACCESS MISSION CONTROL";
            else if (reason.StartsWith("suspended"))
            {
                string suspendedUntil = reason.Split(';')[1];
                string suspendedFor = reason.Split(';')[2];

                if (suspendedUntil != "FOREVER")
                {
                    DateTime suspendedUntilTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(suspendedUntil)).DateTime;
                    suspendedUntil = suspendedUntilTime.ToString("yyyy/MM/dd HH:mm");
                }

                error.Text = $"YOUR ACCOUNT IS SUSPENDED UNTIL {suspendedUntil}.\n{suspendedFor}";
            }
            else
                error.Text = reason;

            button_connect.Visibility = Visibility.Visible;
            button_connect_loading.IsLoading = false;
            button_connect_loading.Visibility = Visibility.Hidden;

            loginInfo.Visibility = Visibility.Visible;
            status.Visibility = Visibility.Collapsed;

            textbox_password.Password = "";
        }

        private void CreateAccountRedirect(object sender, System.Windows.Input.MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo() { FileName = "https://bfmeladder.com/register", UseShellExecute = true });

        private void ResetPasswordRedirect(object sender, System.Windows.Input.MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo() { FileName = "https://bfmeladder.com/reset_password", UseShellExecute = true });
    }
}
