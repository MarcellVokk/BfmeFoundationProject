using BfmeFoundationProject.BfmeKit.Data;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.BfmeKit.Utils
{
    internal class WorkshopUtils
    {
        [SupportedOSPlatform("windows")]
        public static string GetActiveModPath(int game)
        {
            try
            {
                if (File.Exists(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), "mod.txt")))
                    return File.ReadAllText(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), "mod.txt"));
            }
            catch { }

            return "";
        }
    }
}
