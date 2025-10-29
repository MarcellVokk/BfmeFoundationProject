using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.HttpInstruments;

namespace BfmeFoundationProject.AllInOneLauncher.Core;

public static class LauncherUpdateManager
{
    public static string LauncherAppDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME All In One Launcher");

    public static async void CheckForUpdates()
    {
#if DEBUG
        return;
#endif

        try
        {
            if (!Directory.Exists(LauncherAppDirectory))
                Directory.CreateDirectory(LauncherAppDirectory);

            string curentVersionHash = "";
            string latestVersionHash = "";

            try
            {
                curentVersionHash = File.Exists(Path.Combine(LauncherAppDirectory, "AllInOneLauncher.exe")) ? await Task.Run(() => FileUtils.GetFileMd5Hash(Path.Combine(LauncherAppDirectory, "AllInOneLauncher.exe"))) : "";
                latestVersionHash = await HttpMarshal.GetString(url: Consts.LATEST_VERSION_SOURCE_URL, headers: []);
            }
            catch
            {
                return;
            }

            if (curentVersionHash == latestVersionHash)
            {
                if (File.Exists(Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe")))
                    File.Delete(Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe"));

                MainWindow.Instance.Dispatcher.Invoke(() =>
                {
                    if (!File.Exists(Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "update_2_complete.txt")))
                    {
                        Properties.Settings.Default.ArenaUpdateBranch = "main";
                        Properties.Settings.Default.Save();

                        PopupVisualizer.ShowPopup(new DonatePopup());

                        File.WriteAllText(Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "update_2_complete.txt"), "1");
                    }
                });

                return;
            }
            else if (File.Exists(Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe")))
            {
                try
                {
                    File.Move(Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe"), Path.Combine(LauncherAppDirectory, "AllInOneLauncher.exe"), true);
                    RestartLauncher(afterUpdate: false);
                }
                catch
                {
                    RestartLauncher(afterUpdate: true);
                }

                return;
            }

            var updateProgressPopup = new ProgressPopup(File.Exists(Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe")) ? "{LauncherUpdatePopupTitle}" : "{LauncherInstallPopupTitle}", "{LauncherUpdatePopupDescription}");
            PopupVisualizer.ShowPopup(updateProgressPopup);

            await HttpMarshal.GetFile(
            url: Consts.LATEST_BUILD_SOURCE_URL,
            localPath: Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe"),
            headers: [],
            OnProgressUpdate: (progress) => updateProgressPopup.Dispatcher.Invoke(() => updateProgressPopup.LoadProgress = progress));

            RestartLauncher(afterUpdate: true);
        }
        catch (Exception ex)
        {
            PopupVisualizer.HidePopup();
            PopupVisualizer.ShowPopup(new ErrorPopup(ex));
        }
    }

    private static void RestartLauncher(bool afterUpdate)
    {
        App.Mutex?.Dispose();
        App.Mutex = null;

        ProcessStartInfo process = new()
        {
            UseShellExecute = true,
            WorkingDirectory = LauncherAppDirectory,
            FileName = afterUpdate
                ? Path.Combine(LauncherAppDirectory, "AllInOneLauncher_new.exe")
                : Path.Combine(LauncherAppDirectory, "AllInOneLauncher.exe"),
            Verb = "runas"
        };
        Process.Start(process);

        Environment.Exit(0);
    }
}