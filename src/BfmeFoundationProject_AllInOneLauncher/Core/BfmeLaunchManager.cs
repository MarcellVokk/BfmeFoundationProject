using System.Diagnostics;
using System.IO;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Core;

internal static class BfmeLaunchManager
{
    internal static void LaunchGame(Data.BfmeGame game)
    {
        LauncherStateManager.Visible = false;

        ProcessStartInfo startInfo = new()
        {
            WorkingDirectory = BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath),
            FileName = Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), BfmeDefaults.DefaultGameExecutableNames[(int)game])
        };

        string activeModPath = BfmeWorkshopManager.GetActiveModPath((int)game);
        if (activeModPath != "")
        {
            startInfo.ArgumentList.Add("-mod");
            startInfo.ArgumentList.Add(activeModPath);
        }

        BfmeRegistryManager.EnsureCompatibilitySettings(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), BfmeDefaults.DefaultGameExecutableNames[(int)game]));
        BfmeRegistryManager.EnsureCompatibilitySettings(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), "game.dat"));

        using Process? gameProcess = Process.Start(startInfo);
        if (gameProcess == null) return;
        gameProcess.WaitForExit();

        LauncherStateManager.Visible = true;
    }
}