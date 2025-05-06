using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public static class BfmeReplayHelper
    {
        [SupportedOSPlatform("windows")]
        public static string GetLastReplayFileName(object game)
        {
            if (Convert.ToInt32(game) > 0)
            {
                if (BfmeRegistryManager.IsInstalled(game))
                {
                    var gameInstallDirectory = BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath);
                    var activeModPath = WorkshopUtils.GetActiveModPath(Convert.ToInt32(game));
                    var src_strings = new List<string>();

                    if (Directory.Exists(activeModPath))
                        src_strings.AddRange(FilenameUtils.OrderFiles(Directory.GetFiles(activeModPath, "*.str", SearchOption.AllDirectories), game).Select(x => File.ReadAllText(x)));

                    if (Directory.Exists(Path.Combine(gameInstallDirectory, "lang")))
                        foreach (var archive in FilenameUtils.OrderFiles(Directory.GetFiles(Path.Combine(gameInstallDirectory, "lang"), "*.big"), game).Select(x => BigArchiveReader.Unpack(x)))
                            src_strings.AddRange(archive.Values.Where(x => Path.GetExtension(x.Name) == ".str").Select(x => x.GetText()));

                    foreach (var archive in FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)))
                        src_strings.AddRange(archive.Values.Where(x => Path.GetExtension(x.Name) == ".str").Select(x => x.GetText()));

                    if (Directory.Exists(Path.Combine(gameInstallDirectory, "lang")))
                        foreach (var archive in FilenameUtils.OrderFiles(Directory.GetFiles(Path.Combine(gameInstallDirectory, "lang"), "*.big"), game).Select(x => BigArchiveReader.Unpack(x)))
                            src_strings.AddRange(archive.Values.Where(x => Path.GetExtension(x.Name) == ".csf").Select(x => CsfConverter.ConvertToStr(x.GetData())));

                    foreach (var archive in FilenameUtils.OrderFiles(Directory.GetFiles(Path.Combine(gameInstallDirectory, "lang"), "*.big"), game).Select(x => BigArchiveReader.Unpack(x)))
                        src_strings.AddRange(archive.Values.Where(x => Path.GetExtension(x.Name) == ".csf").Select(x => CsfConverter.ConvertToStr(x.GetData())));

                    var blockId = "";
                    var blockContent = "";

                    foreach (var str in src_strings)
                    {
                        blockId = "";
                        blockContent = "";
                        foreach (var line in str.Split('\n').Select(x => x.Trim('\n').Trim('\r').Trim(' ').Trim('	').Trim(' ').Trim('	')))
                        {
                            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith(";"))
                                continue;

                            if (blockId == "")
                            {
                                blockId = line;
                                blockContent = "";
                            }
                            else if (line.ToLower().StartsWith("end"))
                            {
                                if (blockId.ToLower() == "gui:lastreplay")
                                    return $"{blockContent.Trim('"')}.BfME2Replay";

                                blockId = "";
                                blockContent = "";
                            }
                            else
                            {
                                blockContent = line;
                            }
                        }
                    }
                }

                return "Last Replay.BfME2Replay";
            }
            else
            {
                return "00000000.rep";
            }
        }
    }
}
