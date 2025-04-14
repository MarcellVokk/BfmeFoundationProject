namespace BfmeFoundationProject.BfmeKit.Utils
{
    internal class FilenameUtils
    {
        public static List<string> OrderFiles(string[] files, object game)
        {
            List<(string fileName, int groupType, int weight)> fileLibrary = new List<(string fileName, int groupType, int weight)>();
            foreach (var file in files)
            {
                if (Path.GetFileNameWithoutExtension(file).StartsWith("!"))
                    fileLibrary.Add((file, 100, Path.GetFileNameWithoutExtension(file).TakeWhile(x => x == '!').Count()));
                else if (Path.GetFileNameWithoutExtension(file).StartsWith("#"))
                    fileLibrary.Add((file, 99, Path.GetFileNameWithoutExtension(file).TakeWhile(x => x == '#').Count()));
                else if (Path.GetFileNameWithoutExtension(file).StartsWith("_"))
                    fileLibrary.Add((file, 98, Path.GetFileNameWithoutExtension(file).TakeWhile(x => x == '_').Count()));
                else
                    fileLibrary.Add((file, 0, 1));
            }

            List<string> result = new List<string>();
            foreach (var group in fileLibrary.GroupBy(x => x.groupType).OrderByDescending(x => x.Key))
            {
                if (Convert.ToInt32(game) == 0)
                {
                    if (group.Key > 0)
                        result.AddRange(group.OrderBy(x => x.weight).OrderByDescending(x => x.fileName).Select(x => x.fileName));
                    else
                        result.AddRange(group.OrderBy(x => x.weight).OrderBy(x => x.fileName).Select(x => x.fileName));
                }
                else
                {
                    result.AddRange(group.OrderByDescending(x => x.weight).OrderBy(x => x.fileName).Select(x => x.fileName));
                }
            }

            return result;
        }
    }
}
