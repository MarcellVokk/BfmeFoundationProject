using System.Drawing;

namespace BfmeFoundationProject.BfmeKit.Data
{
    public struct BfmeColor
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Color PreviewColor { get; set; }

        public BfmeColor(string name, int id, Color previewColor)
        {
            Name = name;
            Id = id;
            PreviewColor = previewColor;
        }
    }
}
