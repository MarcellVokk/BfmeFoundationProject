using System.Text;

namespace BfmeFoundationProject.BfmeKit.Data
{
    public struct BigFile
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }

        public BigFile(string name, string source, int offset, int size)
        {
            Name = name;
            Source = source;
            Offset = offset;
            Size = size;
        }

        public byte[] GetData()
        {
            using (FileStream fsSource = new FileStream(Source, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                byte[] bytes = new byte[Size];

                fsSource.Seek(Offset, SeekOrigin.Begin);
                fsSource.Read(bytes, 0, Size);

                return bytes;
            }
        }

        public string GetText() => Encoding.GetEncoding("iso-8859-4").GetString(GetData());
    }
}
