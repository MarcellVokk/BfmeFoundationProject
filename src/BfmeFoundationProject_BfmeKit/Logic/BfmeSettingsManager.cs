using BfmeFoundationProject.BfmeKit.Data;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    [SupportedOSPlatform("windows")]
    public static class BfmeSettingsManager
    {
        public static string? Get(object game, string optionName)
        {
            string optionsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.UserDataLeafName), "Options.ini");
            Dictionary<string, string> optionsTable = File.Exists(optionsFile) ? File.ReadAllText(optionsFile).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0].Trim('\r'), x => x.Split(" = ")[1].Trim('\r')) : [];

            if (optionsTable.TryGetValue(optionName, out string? value))
                return value;
            else
                return null;
        }

        public static void Set(object game, string optionName, string value)
        {
            string optionsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.UserDataLeafName), "Options.ini");
            Dictionary<string, string> optionsTable = (File.Exists(optionsFile) ? File.ReadAllText(optionsFile) : BfmeDefaults.DefaultOptions).Split('\n').Where(x => x.Contains(" = ")).ToDictionary(x => x.Split(" = ")[0].Trim('\r'), x => x.Split(" = ")[1].Trim('\r'));

            if (!optionsTable.TryAdd(optionName, value))
                optionsTable[optionName] = value;

            File.WriteAllText(optionsFile, string.Join('\n', optionsTable.Select(x => $"{x.Key} = {x.Value}")));
        }
    }
}
