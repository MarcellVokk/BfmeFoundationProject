using System.Drawing;

namespace BfmeFoundationProject.BfmeKit.Logic
{
    internal static class RectUtils
    {
        public static SizeF ResizeToFit(SizeF sourceSize, SizeF targetSize)
        {
            double widthScale = targetSize.Width / sourceSize.Width;
            double heightScale = targetSize.Height / sourceSize.Height;
            double scale = Math.Min(widthScale, heightScale);

            double newWidth = sourceSize.Width * scale;
            double newHeight = sourceSize.Height * scale;

            return new SizeF((float)newWidth, (float)newHeight);
        }
    }
}
