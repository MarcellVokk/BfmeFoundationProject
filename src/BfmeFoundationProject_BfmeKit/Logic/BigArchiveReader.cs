using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Utils;
using System.Text;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    public static class BigArchiveReader
    {
        public static Dictionary<string, BigFile> Unpack(string sourceBigArchive, bool throwOnError = false)
        {
            Dictionary<string, BigFile> files = new Dictionary<string, BigFile>();

            try
            {
                using (var reader = new BinaryReader(new FileStream(sourceBigArchive, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.Latin1, false))
                {
                    if (reader.BaseStream.Length == 0)
                        return files;

                    var id = reader.ReadFixedLengthString(4);
                    var file_size = reader.ReadUInt32();
                    var n_files = reader.ReadUInt32_BigEndian();
                    var index_table_size = reader.ReadUInt32_BigEndian();

                    for (int i = 0; i < n_files; i++)
                    {
                        var embeded_file_offset = reader.ReadUInt32_BigEndian();
                        var embeded_file_size = reader.ReadUInt32_BigEndian();
                        var embeded_file_name = reader.ReadNullTerminatedString().ToLower();

                        if (!files.ContainsKey(embeded_file_name))
                            files.Add(embeded_file_name, new BigFile(embeded_file_name, sourceBigArchive, (int)embeded_file_offset, (int)embeded_file_size));
                    }
                }
            }
            catch
            {
                if (throwOnError)
                    throw;
                else
                    return new Dictionary<string, BigFile>();
            }

            return files;
        }

        public static string DecodeString(string str)
        {
            return str
                .Replace("_5C", @"\")
                .Replace("_3A", ":")
                .Replace("_2E", ".")
                .Replace("_20", " ")
                .Replace("_24", "$")
                .Replace("_00", "")
                .Replace("_5F", "_")
                .Replace("_2D", "-")
                .Replace("_28", "(")
                .Replace("_29", ")")
                .Replace("_5B", "[")
                .Replace("_5B", "[")
                .Replace("_5D", "]")
                .Replace("_21", "!")
                .Replace("_27", "'");
        }
    }
}
