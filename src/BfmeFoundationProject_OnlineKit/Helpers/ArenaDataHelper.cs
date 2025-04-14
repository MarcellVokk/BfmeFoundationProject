using System.IO;

namespace BfmeFoundationProject.OnlineKit.Helpers
{
    public static class ArenaDataHelper
    {
        public static void EnsureDirectories()
        {
            if (!Directory.Exists(GlobalInstallPath))
                Directory.CreateDirectory(GlobalInstallPath);

            if (!Directory.Exists(GlobalDataPath))
                Directory.CreateDirectory(GlobalDataPath);
        }

        public static bool IsInstalled => File.Exists(Path.Combine(GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"));
        public static string GlobalInstallPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena");
        public static string GlobalDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Competetive Arena", "Data");
    }
}
