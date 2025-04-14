using System.Windows;
using System.Windows.Media;

namespace BfmeFoundationProject.OnlineKit.Helpers
{
    public static class SizeHelper
    {
        public static Rect? OverrideDisplayRect = null;

        public static Rect GetAbsoluteRect(FrameworkElement element, bool accountForDpi = false)
        {
            if (OverrideDisplayRect is not null)
                return OverrideDisplayRect.Value;

            try
            {
                var absolutePos = element.PointToScreen(new Point(0, 0));
                var absoluteBottomRightPos = element.PointToScreen(new Point(element.ActualWidth, element.ActualHeight));
                var posMW = Window.GetWindow(element).PointToScreen(new Point(0, 0));

                if (accountForDpi)
                {
                    var dpi = VisualTreeHelper.GetDpi(element);

                    absolutePos.X /= dpi.DpiScaleX;
                    absolutePos.Y /= dpi.DpiScaleY;

                    absoluteBottomRightPos.X /= dpi.DpiScaleX;
                    absoluteBottomRightPos.Y /= dpi.DpiScaleY;

                    posMW.X /= dpi.DpiScaleX;
                    posMW.Y /= dpi.DpiScaleY;
                }

                absolutePos = new Point(absolutePos.X - posMW.X, absolutePos.Y - posMW.Y);
                absoluteBottomRightPos = new Point(absoluteBottomRightPos.X - posMW.X, absoluteBottomRightPos.Y - posMW.Y);

                if (element.IsVisible)
                    return new Rect(absolutePos.X, absolutePos.Y, absoluteBottomRightPos.X - absolutePos.X, absoluteBottomRightPos.Y - absolutePos.Y);
                else
                    return new Rect(absolutePos.X, absolutePos.Y, 0, 0);
            }
            catch { }

            return Rect.Empty;
        }
    }
}
