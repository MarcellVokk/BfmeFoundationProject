using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.IO.Pipes;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using WindowsShortcutFactory;
using Microsoft.Win32;
using System.Diagnostics;
using BfmeFoundationProject.AllInOneLauncher.Core;

namespace BfmeFoundationProject.AllInOneLauncher
{
    public partial class App : Application
    {
        internal static Mutex? Mutex;
        internal static string[] Args = [];
        private string _mutexName;
        private string _pipeName;

        public App()
        {
            _mutexName = ConfigurationManager.AppSettings["mutexName"] ?? 
                         throw new ArgumentNullException($"mutexName needs to be specified inside the appsettings");
            _pipeName = ConfigurationManager.AppSettings["pipeName"] ?? 
                        throw new ArgumentNullException($"pipeName needs to be specified inside the appsettings");
        }

        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
#else
            if (File.Exists(Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe")) && Path.GetDirectoryName(Environment.ProcessPath)!.ToLower().Trim('\\').Trim('/') != LauncherUpdateManager.LauncherAppDirectory.ToLower().Trim('\\').Trim('/'))
            {
                MessageBox.Show("You already have the All In One Launcher installed. Open it from the shortcut on your desktop!", "Launcher already installed", MessageBoxButton.OK);
                ExitImmediately();
                return;
            }
#endif

            Mutex = new Mutex(true, _mutexName, out bool launcherNotOpenAlready);
            bool launcherOpenAlready = !launcherNotOpenAlready;
            Args = Environment.GetCommandLineArgs().Skip(1).ToArray();

            if (Args.Length > 0 && Args[0] == "--Uninstall")
            {
                Uninstall();
                foreach (var p in Process.GetProcessesByName("AllInOneLauncher"))
                    p.Kill();
                return;
            }

            if (launcherOpenAlready)
            {
                try
                {
                    using var client = new NamedPipeClientStream(".", _pipeName, PipeDirection.Out);
                    client.Connect(3000);
                    Thread.Sleep(500);
                }
                catch { }
                ExitImmediately();
                return;
            }

            base.OnStartup(e);

            StartServer();
            EnsureShortcut();
            EnsureAppConfig();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void StartServer()
        {
            Task.Run(() =>
            {
                using var server = new NamedPipeServerStream(
                    _pipeName, 
                    PipeDirection.In, 
                    1, 
                    PipeTransmissionMode.Byte, 
                    PipeOptions.Asynchronous);

                while (true)
                {
                    try
                    {
                        server.WaitForConnection();
                        Dispatcher.Invoke(() => LauncherStateManager.Activate());
                        server.Disconnect();
                    }
                    catch { }
                }
            });
        }

        private void EnsureShortcut()
        {
            if (!BfmeFoundationProject.AllInOneLauncher.Properties.Settings.Default.CreateDesktopShortcut)
                return;

            using var shortcut = new WindowsShortcut
            {
                Path = Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe"),
                Description = "All-in-One Launcher"
            };

            try
            {
                shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "All in One Launcher.lnk"));
            }
            catch { }
        }

        private void EnsureAppConfig()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "All In One Launcher")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "All In One Launcher"));

                using var shortcut = new WindowsShortcut
                {
                    Path = Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe"),
                    Description = "All-in-One Launcher"
                };
                shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "All In One Launcher", "All in One Launcher.lnk"));
            }
            catch { }

            try
            {
                if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher")))
                    Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Patch 2.22 Launcher"), true);
            }
            catch { }

            try
            {
                try
                {
                    using RegistryKey? keyOldApp = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\All In One Launcher_is1", false);
                    string oldInstallLocation = keyOldApp?.GetValue("InstallLocation") as string ?? "";

                    if (Directory.Exists(oldInstallLocation))
                        Directory.Delete(oldInstallLocation, true);

                    Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\All In One Launcher_is1", false);
                }
                catch { }

                using RegistryKey? keyApp = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\All In One Launcher", true);
                keyApp?.SetValue("DisplayIcon", Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe"));
                keyApp?.SetValue("DisplayName", "All In One Launcher");
                keyApp?.SetValue("DisplayVersion", BuildInfo.BuildIdentifier);
                keyApp?.SetValue("Publisher", "The Bfme Foundation Team");
                keyApp?.SetValue("EstimatedSize", "400000", RegistryValueKind.DWord);
                keyApp?.SetValue("InstallDate", DateTime.Today);
                keyApp?.SetValue("InstallLocation", LauncherUpdateManager.LauncherAppDirectory);
                keyApp?.SetValue("VersionMajor", BuildInfo.BuildIdentifier.Split('.')[0], RegistryValueKind.DWord);
                keyApp?.SetValue("VersionMinor", BuildInfo.BuildIdentifier.Split('.')[1], RegistryValueKind.DWord);
                keyApp?.SetValue("Version", $"{BuildInfo.BuildIdentifier.Split('.')[2]}{BuildInfo.BuildIdentifier.Split('.')[3]}", RegistryValueKind.DWord);
                keyApp?.SetValue("UninstallString", $"{Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe")} --Uninstall");
            }
            catch { }

            try
            {
                Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Patch 2.22 Launcher_is1", false);
            }
            catch { }
        }

        public void Uninstall()
        {
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "All in One Launcher.lnk")))
                File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "All in One Launcher.lnk"));

            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "All In One Launcher")))
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "All In One Launcher"), true);

            Registry.LocalMachine.DeleteSubKeyTree(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\All In One Launcher", false);

            MessageBox.Show("The Launcher was uninstalled successfuly.", "Uninstall complete", MessageBoxButton.OK);

            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe") + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (Mutex is not null)
            {
                if (Mutex.WaitOne(TimeSpan.Zero, true))
                    Mutex.ReleaseMutex();

                Mutex.Dispose();
            }

            base.OnExit(e);
        }

        public static void ExitImmediately()
        {
            Mutex?.Dispose();
            Mutex = null;
            Environment.Exit(0);
        }
    }
}