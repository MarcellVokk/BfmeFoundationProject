using BfmeFoundationProject.BfmeKit.Utils;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public static class CsfConverter
    {
        public static string ConvertToStr(byte[] source)
        {
            Dictionary<string, string> strings = new Dictionary<string, string>();

            using (var reader = new BinaryReader(new MemoryStream(source)))
            {
                if (reader.BaseStream.Length == 0)
                    return "";

                var id = reader.ReadFixedLengthString(4);
                var version = reader.ReadUInt32();
                var n_labels = reader.ReadUInt32();
                var n_strings = reader.ReadUInt32();
                var extra_tag = reader.ReadUInt32();
                var lang = reader.ReadUInt32();

                for (int i = 0; i < n_labels; i++)
                {
                    var lbl = reader.ReadFixedLengthString(4);
                    var n_pairs = reader.ReadUInt32();
                    var name_length = reader.ReadUInt32();
                    var name = reader.ReadFixedLengthString((int)name_length);

                    for (int p = 0; p < n_pairs; p++)
                    {
                        var lid = reader.ReadFixedLengthString(4);
                        var value_length = reader.ReadUInt32();
                        var value = reader.ReadFixedLengthStringUnicode((int)value_length);

                        if (!strings.ContainsKey(name))
                            strings.Add(name, value.Replace("\n", "\\n"));
                    }
                }
            }

            return string.Join("\n", strings.Select(x => $"{x.Key}\n\"{x.Value}\"\nEND\n"));
        }
    }
}
