using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Menues;

public class ContextMenuSeparatorItem : ContextMenuItem
{
    public override FrameworkElement GenerateElement(ContextMenuShell shell)
    {
        return new Border() { Height = 1, Margin = new Thickness(0, 3, 0, 3), Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF")), UseLayoutRounding = true, SnapsToDevicePixels = true };
    }
}