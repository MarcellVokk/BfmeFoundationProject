using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Utils;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.WorkshopKit.Logic
{
    public static class BfmeWorkshopManager
    {
        public static string WorkshopServerHost = "https://bfmeladder.com";
        public static string WorkshopFilesHost = "https://workshop-files.bfmeladder.com";

        [SupportedOSPlatform("windows")]
        public static async Task<BfmeWorkshopEntry?> GetActivePatch(object game) => await Task.Run(() => FileUtils.ReadJson<BfmeWorkshopEntry?>(Path.Combine(ConfigUtils.ConfigDirectory, $"active_patch_{Convert.ToInt32(game)}.json"), null));
        [SupportedOSPlatform("windows")]
        public static async Task<Dictionary<string, BfmeWorkshopEntry>> GetActiveEnhancements(object game) => await Task.Run(() => FileUtils.ReadJson(Path.Combine(ConfigUtils.ConfigDirectory, $"active_enhancements_{Convert.ToInt32(game)}.json"), new Dictionary<string, BfmeWorkshopEntry>()));
        [SupportedOSPlatform("windows")]
        public static string GetActiveModPath(object game) => FileUtils.ReadText(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), "mod.txt"), "");

        [SupportedOSPlatform("windows")]
        public static bool IsPatchActive(object game, string entryGuid) => FileUtils.Contains(Path.Combine(ConfigUtils.ConfigDirectory, $"active_patch_{Convert.ToInt32(game)}.json"), $"\"Guid\": \"{entryGuid}\"");
        [SupportedOSPlatform("windows")]
        public static bool IsEnhancementActive(object game, string entryGuid) => FileUtils.Contains(Path.Combine(ConfigUtils.ConfigDirectory, $"active_enhancements_{Convert.ToInt32(game)}.json"), $"\"Guid\": \"{entryGuid}\"");
    }
}
