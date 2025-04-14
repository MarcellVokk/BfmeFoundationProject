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
        public static async Task<BfmeWorkshopEntry?> GetActivePatch(int game) => await Task.Run(() => FileUtils.ReadJson<BfmeWorkshopEntry?>(Path.Combine(ConfigUtils.ConfigDirectory, $"active_patch_{game}.json"), null));
        [SupportedOSPlatform("windows")]
        public static async Task<Dictionary<string, BfmeWorkshopEntry>> GetActiveEnhancements(int game) => await Task.Run(() => FileUtils.ReadJson(Path.Combine(ConfigUtils.ConfigDirectory, $"active_enhancements_{game}.json"), new Dictionary<string, BfmeWorkshopEntry>()));
        [SupportedOSPlatform("windows")]
        public static string GetActiveModPath(int game) => FileUtils.ReadText(Path.Combine(BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath), "mod.txt"), "");

        [SupportedOSPlatform("windows")]
        public static bool IsPatchActive(int game, string entryGuid) => FileUtils.Contains(Path.Combine(ConfigUtils.ConfigDirectory, $"active_patch_{game}.json"), $"\"Guid\": \"{entryGuid}\"");
        [SupportedOSPlatform("windows")]
        public static bool IsEnhancementActive(int game, string entryGuid) => FileUtils.Contains(Path.Combine(ConfigUtils.ConfigDirectory, $"active_enhancements_{game}.json"), $"\"Guid\": \"{entryGuid}\"");
    }
}
