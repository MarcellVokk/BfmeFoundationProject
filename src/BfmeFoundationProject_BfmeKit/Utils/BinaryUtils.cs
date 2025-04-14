using System.Text;

namespace BfmeFoundationProject.BfmeKit.Utils
{
    internal static class BinaryUtils
    {
        static BinaryUtils()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public static uint ReadUInt32_BigEndian(this BinaryReader reader)
        {
            return BitConverter.ToUInt32(reader.ReadBytes(4).Reverse().ToArray());
        }

        public static void WriteUInt32_BigEndian(this BinaryWriter writer, uint value)
        {
            writer.Write(BitConverter.GetBytes(value).Reverse().ToArray());
        }

        public static string ReadNullTerminatedString(this BinaryReader reader)
        {
            string s = "";

            char c;
            while ((c = reader.ReadChar()) != '\0')
                s += c;

            return s;
        }

        public static string ReadFixedLengthString(this BinaryReader reader, int count)
        {
            if (count == 0)
                return string.Empty;

            byte[] chars = reader.ReadBytes(count);
            return Encoding.UTF8.GetString(chars);
        }

        public static string ReadFixedLengthStringUnicode(this BinaryReader reader, int count)
        {
            if (count == 0)
                return string.Empty;

            byte[] chars = reader.ReadBytes(count * 2);
            for (int i = 0; i < chars.Length; i++)
                chars[i] = (byte)~chars[i];

            return Encoding.Unicode.GetString(chars);
        }
    }
}
