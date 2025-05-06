using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public class BfmeMapImporter
    {
        [SupportedOSPlatform("windows")]
        public static List<BfmeMap> ImportMaps(object game)
        {
            var results = new List<BfmeMap>();

            if (!BfmeRegistryManager.IsInstalled(game))
                return results;

            var gameInstallDirectory = BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath);
            var activeModPath = WorkshopUtils.GetActiveModPath(Convert.ToInt32(game));
            var available_maps = new List<string>();
            var src_strings = new List<string>();
            var parsed_strings = new Dictionary<string, string>();
            var mapcache = "";

            if (File.Exists(Path.Combine(activeModPath, "maps", "mapcache.ini")))
                mapcache += File.ReadAllText(Path.Combine(activeModPath, "maps", "mapcache.ini"));
            if (FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).Any(x => x.ContainsKey(@"maps\mapcache.ini")))
                mapcache += FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)).First(x => x.ContainsKey(@"maps\mapcache.ini"))[@"maps\mapcache.ini"].GetText();
            if (mapcache == "")
                return results;

            if (Directory.Exists(activeModPath))
                available_maps.AddRange(FilenameUtils.OrderFiles(Directory.GetFiles(activeModPath, "*.map", SearchOption.AllDirectories), game).Select(x => x.Replace(activeModPath, "").Trim('/').Trim('\\')));

            foreach (var archive in FilenameUtils.OrderFiles(Directory.GetFiles(gameInstallDirectory, "*.big"), game).Select(x => BigArchiveReader.Unpack(x)))
                available_maps.AddRange(archive.Values.Where(x => Path.GetExtension(x.Name) == ".map").Select(x => x.Name));

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

            var inBlock = false;
            var blockValid = true;
            var blockName = "";
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
                        if (!parsed_strings.ContainsKey(blockId.ToLower()))
                            parsed_strings.Add(blockId.ToLower(), blockContent);

                        blockId = "";
                        blockContent = "";
                    }
                    else
                    {
                        blockContent = line;
                    }
                }
            }

            inBlock = false;
            blockValid = true;
            blockName = "";
            blockId = "";
            blockContent = "";
            foreach (var line in mapcache.Split('\n').Select(x => x.Trim('\n').Trim('\r').Trim(' ').Trim('\t').Trim(' ').Trim('\t')))
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith(";"))
                    continue;

                if (line.ToLower().StartsWith("mapcache"))
                {
                    inBlock = true;
                    blockId = line.Remove(0, "mapcache ".Length);
                }
                else if (inBlock)
                {
                    var safeLine = line.Replace("\t", "  ");
                    while (safeLine.Contains("  "))
                        safeLine = safeLine.Replace("  ", " ");
                    safeLine = safeLine.Trim(' ');

                    if (safeLine.ToLower().StartsWith("displayname"))
                        blockName = BigArchiveReader.DecodeString(safeLine.Replace("displayname = ", "").Replace("displayName = ", "")).TrimStart('$');
                    else if (safeLine.ToLower().StartsWith("ismultiplayer"))
                        blockValid = blockValid && safeLine.ToLower().Contains("ismultiplayer = yes");
                    else if (safeLine.ToLower().StartsWith("isofficial"))
                        blockValid = blockValid && safeLine.ToLower().Contains("isofficial = yes");

                    if (safeLine.ToLower().StartsWith("end"))
                    {
                        if (blockValid)
                        {
                            if (blockName.ToLower().StartsWith("map:") && parsed_strings.ContainsKey(blockName.ToLower()))
                                blockName = parsed_strings[blockName.ToLower()].Trim('"');

                            if (!results.Any(x => x.Id == blockId) && available_maps.Contains(BigArchiveReader.DecodeString(blockId)))
                                results.Add(new BfmeMap(blockId, blockName, Convert.ToInt32(game), "", GetMapSize(blockContent).Width, GetMapSize(blockContent).Height, GetMapSpots(blockContent) ?? new List<BfmeSpot>()));
                        }

                        inBlock = false;
                        blockValid = true;
                        blockName = "";
                        blockId = "";
                        blockContent = "";
                    }
                    else
                    {
                        blockContent += (blockContent != "" ? "\n" : "") + BigArchiveReader.DecodeString(safeLine);
                    }
                }
            }

            return results.OrderBy(x => x.Name).OrderBy(x => x.Spots.Count).ToList();
        }

        [SupportedOSPlatform("windows")]
        public static Bitmap? GenerateMapPreview(BfmeMap map)
        {
            using var scrollshroud = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream($"BfmeFoundationProject.BfmeKit.Resources.scrollshroud.png")!);
            using var mapframe = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream($"BfmeFoundationProject.BfmeKit.Resources.{map.Game}-mapframe.png")!);
            Bitmap? mapimage = null;

            scrollshroud.SetResolution(1, 1);
            mapframe.SetResolution(1, 1);

            var decodedMapId = BigArchiveReader.DecodeString(map.Id).ToLower();
            if (decodedMapId.EndsWith(".map"))
                decodedMapId = decodedMapId.Remove(decodedMapId.Length - 4, 4);

            try
            {
                var mapStack = new Bitmap(mapframe.Width, mapframe.Height);
                mapStack.SetResolution(1, 1);

                var activeModPath = WorkshopUtils.GetActiveModPath(Convert.ToInt32(map.Game));

                if (File.Exists(Path.Combine(activeModPath, $"{decodedMapId}_art.tga")))
                {
                    var mapPreviewData = File.ReadAllBytes(Path.Combine(activeModPath, $"{decodedMapId}_art.tga"));
                    mapimage = TgaDecoder.Decode(mapPreviewData);
                    mapimage.SetResolution(1, 1);
                    mapimage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                else if (BfmeRegistryManager.IsInstalled(map.Game) && FilenameUtils.OrderFiles(Directory.GetFiles(BfmeRegistryManager.GetKeyValue(map.Game, BfmeRegistryKey.InstallPath), "*.big"), map.Game).Select(x => BigArchiveReader.Unpack(x)).Any(x => x.ContainsKey($"{decodedMapId}_art.tga")))
                {
                    var mapPreviewData = FilenameUtils.OrderFiles(Directory.GetFiles(BfmeRegistryManager.GetKeyValue(map.Game, BfmeRegistryKey.InstallPath), "*.big"), map.Game).Select(x => BigArchiveReader.Unpack(x)).First(x => x.ContainsKey($"{decodedMapId}_art.tga"))[$"{decodedMapId}_art.tga"].GetData();
                    mapimage = TgaDecoder.Decode(mapPreviewData);
                    mapimage.SetResolution(1, 1);
                    mapimage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                else if (map.Game == 2 && BfmeRegistryManager.IsInstalled(1) && FilenameUtils.OrderFiles(Directory.GetFiles(BfmeRegistryManager.GetKeyValue(1, BfmeRegistryKey.InstallPath), "*.big"), 1).Select(x => BigArchiveReader.Unpack(x)).Any(x => x.ContainsKey($"{decodedMapId}_art.tga")))
                {
                    var mapPreviewData = FilenameUtils.OrderFiles(Directory.GetFiles(BfmeRegistryManager.GetKeyValue(1, BfmeRegistryKey.InstallPath), "*.big"), 1).Select(x => BigArchiveReader.Unpack(x)).First(x => x.ContainsKey($"{decodedMapId}_art.tga"))[$"{decodedMapId}_art.tga"].GetData();
                    mapimage = TgaDecoder.Decode(mapPreviewData);
                    mapimage.SetResolution(1, 1);
                    mapimage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                }
                else
                {
                    return mapStack;
                }

                var scaledMapSize = RectUtils.ResizeToFit(new SizeF((float)map.Width, (float)map.Height), new SizeF(mapStack.Width - 20, mapStack.Height - 20));
                var mapContentRect = new Rectangle((int)(mapStack.Width / 2 - scaledMapSize.Width / 2), (int)(mapStack.Height / 2 - scaledMapSize.Height / 2), (int)scaledMapSize.Width, (int)scaledMapSize.Height);

                using (var g = Graphics.FromImage(mapStack))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    g.FillRectangle(Brushes.Black, new Rectangle(0, 0, mapStack.Width, mapStack.Height));
                    g.DrawImage(scrollshroud, mapContentRect);
                    g.DrawImage(mapimage, mapContentRect);

                    g.DrawImage(mapframe, new Point(0, 0));

                    var p = new Pen(Color.FromArgb(150, 110, 0), 4);
                    var s = new Pen(Color.Black, 2);

                    foreach (BfmeSpot spot in map.Spots)
                    {
                        g.FillEllipse(Brushes.Black, spot.X * mapStack.Width - 32 / 2, spot.Y * mapStack.Height - 32 / 2, 32, 32);
                        g.DrawEllipse(p, spot.X * mapStack.Width - 32 / 2, spot.Y * mapStack.Height - 32 / 2, 32, 32);
                        g.DrawEllipse(s, spot.X * mapStack.Width - 38 / 2, spot.Y * mapStack.Height - 38 / 2, 38, 38);
                    }
                }

                mapimage?.Dispose();

                return mapStack;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Creating map preview failed -> " + decodedMapId);
                Debug.WriteLine(ex.ToString());
            }

            mapimage?.Dispose();

            return null;
        }

        private static SizeF GetMapSize(string mapConfigObject)
        {
            var mapConfig = ParseMapConfigObject(mapConfigObject);
            if (!mapConfig.ContainsKey("extentMax"))
                return SizeF.Empty;

            return new SizeF(float.Parse(mapConfig["extentMax"].Split(' ')[0].Replace("X:", ""), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(mapConfig["extentMax"].Split(' ')[1].Replace("Y:", ""), NumberStyles.Any, CultureInfo.InvariantCulture));
        }

        private static List<BfmeSpot>? GetMapSpots(string mapConfigObject)
        {
            var mapConfig = ParseMapConfigObject(mapConfigObject);
            if (!mapConfig.ContainsKey("extentMax"))
                return null;

            var originalMapSize = new SizeF(float.Parse(mapConfig["extentMax"].Split(' ')[0].Replace("X:", ""), NumberStyles.Any, CultureInfo.InvariantCulture), float.Parse(mapConfig["extentMax"].Split(' ')[1].Replace("Y:", ""), NumberStyles.Any, CultureInfo.InvariantCulture));
            var scaledMapSize = RectUtils.ResizeToFit(originalMapSize, new SizeF(3470f, 2600f));

            var spotCount = 1;
            while (mapConfig.ContainsKey($"Player_{spotCount}_Start"))
                spotCount++;

            var spots = new List<BfmeSpot>();
            for (var i = 1; i <= 8; i++)
            {
                if (!mapConfig.ContainsKey($"Player_{i}_Start"))
                    break;

                var x = float.Parse(mapConfig[$"Player_{i}_Start"].Split(' ')[0].Replace("X:", ""), NumberStyles.Any, CultureInfo.InvariantCulture) / originalMapSize.Width;
                var y = float.Parse(mapConfig[$"Player_{i}_Start"].Split(' ')[1].Replace("Y:", ""), NumberStyles.Any, CultureInfo.InvariantCulture) / originalMapSize.Height;

                if (y > 0.5)
                    y = 0.5f - (y - 0.5f);
                else if (y < 0.5)
                    y = 0.5f + (0.5f - y);

                spots.Add(new BfmeSpot((x * scaledMapSize.Width + 3470f / 2 - scaledMapSize.Width / 2f) / 3470f, (y * scaledMapSize.Height + 2600f / 2 - scaledMapSize.Height / 2f) / 2600f, (i <= spotCount / 2) ? 0 : 1, i - 1));
            }

            return spots;
        }

        private static Dictionary<string, string> ParseMapConfigObject(string configObject)
        {
            var fields = new Dictionary<string, string>();
            foreach (var line in configObject.Split('\n'))
            {
                if (line.Contains(" = ") && !fields.ContainsKey(line.Split(" = ")[0]))
                    fields.Add(line.Split(" = ")[0], line.Split(" = ")[1].Replace("\n", "").Replace("\r", ""));
            }

            return fields;
        }
    }
}
