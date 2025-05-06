using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Utils;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public static class BfmeFactionImporter
    {
        [SupportedOSPlatform("windows")]
        public static List<BfmeFaction> ImportFactions(object game)
        {
            var results = new List<BfmeFaction>();

            if (!BfmeRegistryManager.IsInstalled(game))
                return results;

            var gameInstallDirectory = BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath);
            var activeModPath = WorkshopUtils.GetActiveModPath(Convert.ToInt32(game));

            var playertemplateini = "";

            if (File.Exists(Path.Combine(activeModPath, "data", "ini", "playertemplate.ini")))
                playertemplateini += File.ReadAllText(Path.Combine(activeModPath, "data", "ini", "playertemplate.ini"));
            else if (FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).Any(x => x.ContainsKey(@"data\ini\playertemplate.ini")))
                playertemplateini = FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).First(x => x.ContainsKey(@"data\ini\playertemplate.ini"))[@"data\ini\playertemplate.ini"].GetText();
            else
                return results;

            var inBlock = false;
            var blockValid = true;
            var blockName = "";
            var blockId = 0;

            foreach (string line in playertemplateini.Split("\n").Select(x => x.Trim('\n').Trim('\r').Trim(' ').Trim('\t').Trim(' ').Trim('\t')))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith(";"))
                    continue;

                if (line.ToLower().StartsWith("playertemplate"))
                {
                    inBlock = true;
                    blockName = line.Replace("PlayerTemplate ", "").Replace("playerTemplate ", "").Replace("playertemplate ", "");
                }
                else if (inBlock)
                {
                    var safeLine = line.Replace("\t", "  ");
                    while (safeLine.Contains("  "))
                        safeLine = safeLine.Replace("  ", " ");
                    safeLine = safeLine.Trim(' ');

                    if (safeLine.ToLower().StartsWith("playableside"))
                        blockValid = blockValid && safeLine.ToLower().Contains("playableside = yes");

                    if (safeLine.ToLower().StartsWith("end"))
                    {
                        if (blockValid)
                        {
                            var faction = new BfmeFaction(blockName, blockId, "", "");

                            if (blockName == "FactionRohan")
                            {
                                faction.BigIcon = "STANDARD:rohan.png";
                                faction.SmallIcon = "STANDARD:rohan.png";
                            }
                            else if (blockName == "FactionGondor")
                            {
                                faction.BigIcon = "STANDARD:gondor.png";
                                faction.SmallIcon = "STANDARD:gondor.png";
                            }
                            else if (blockName == "FactionIsengard")
                            {
                                faction.BigIcon = "STANDARD:isengard.png";
                                faction.SmallIcon = "STANDARD:isengard.png";
                            }
                            else if (blockName == "FactionMordor")
                            {
                                faction.BigIcon = "STANDARD:mordor.png";
                                faction.SmallIcon = "STANDARD:mordor.png";
                            }
                            else if (blockName == "FactionMen")
                            {
                                faction.BigIcon = "STANDARD:men.png";
                                faction.SmallIcon = "STANDARD:men.png";
                            }
                            else if (blockName == "FactionElves")
                            {
                                faction.BigIcon = "STANDARD:elves.png";
                                faction.SmallIcon = "STANDARD:elves.png";
                            }
                            else if (blockName == "FactionDwarves")
                            {
                                faction.BigIcon = "STANDARD:dwarves.png";
                                faction.SmallIcon = "STANDARD:dwarves.png";
                            }
                            else if (blockName == "FactionWild")
                            {
                                faction.BigIcon = "STANDARD:goblins.png";
                                faction.SmallIcon = "STANDARD:goblins.png";
                            }
                            else if (blockName == "FactionAngmar")
                            {
                                faction.BigIcon = "STANDARD:angmar.png";
                                faction.SmallIcon = "STANDARD:angmar.png";
                            }
                            else
                            {
                                faction.BigIcon = "STANDARD:rohan.png";
                                faction.SmallIcon = "STANDARD:rohan.png";
                            }

                            results.Add(faction);
                        }

                        inBlock = false;
                        blockValid = true;
                        blockName = "";
                        blockId++;
                    }
                }
            }

            return results;
        }
    }
}
