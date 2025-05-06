using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Utils;
using System.Drawing;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public static class BfmeColorImporter
    {
        [SupportedOSPlatform("windows")]
        public static List<BfmeColor> ImportColors(object game)
        {
            var results = new List<BfmeColor>();

            if (!BfmeRegistryManager.IsInstalled(game))
                return results;

            var gameInstallDirectory = BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath);
            var activeModPath = WorkshopUtils.GetActiveModPath(Convert.ToInt32(game));

            var multiplayerini = "";

            if (File.Exists(Path.Combine(activeModPath, "data", "ini", "multiplayer.ini")))
                multiplayerini += File.ReadAllText(Path.Combine(activeModPath, "data", "ini", "multiplayer.ini"));
            else if (FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).Any(x => x.ContainsKey(@"data\ini\multiplayer.ini")))
                multiplayerini = FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).First(x => x.ContainsKey(@"data\ini\multiplayer.ini"))[@"data\ini\multiplayer.ini"].GetText();
            else
                return results;

            var inBlock = false;
            var blockName = "";
            var blockId = 0;
            var blockContent = "";

            foreach (var line in multiplayerini.Split("\n").Select(x => x.Trim('\n').Trim('\r').Trim(' ').Trim('\t').Trim(' ').Trim('\t')))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith(";"))
                    continue;

                if (line.ToLower().StartsWith("multiplayercolor"))
                {
                    inBlock = true;
                    blockName = line.Replace("MultiplayerColor ", "").Replace("multiplayerColor ", "").Replace("multiplayercolor ", "");
                }
                else if (inBlock)
                {
                    var safeLine = line.Replace("\t", "  ");
                    while (safeLine.Contains("  "))
                        safeLine = safeLine.Replace("  ", " ");
                    safeLine = safeLine.Trim(' ');

                    if (safeLine.ToLower().StartsWith("rgbcolor"))
                        blockContent = safeLine.ToLower().Replace("rgbcolor =", "").Replace("rgbcolor= ", "").Trim(' ');

                    if (safeLine.ToLower().StartsWith("end"))
                    {
                        var blockContentParts = blockContent.Split([' ', ';']);
                        try { results.Add(new BfmeColor(blockName.StartsWith("Color") ? blockName.Remove(0, "Color".Length) : blockName, blockId, Color.FromArgb(int.Parse(blockContentParts[0].Replace("r:", "")), int.Parse(blockContentParts[1].Replace("g:", "")), int.Parse(blockContentParts[2].Replace("b:", ""))))); } catch { }

                        inBlock = false;
                        blockName = "";
                        blockId++;
                        blockContent = "";
                    }
                }
            }

            return results;
        }
    }
}
