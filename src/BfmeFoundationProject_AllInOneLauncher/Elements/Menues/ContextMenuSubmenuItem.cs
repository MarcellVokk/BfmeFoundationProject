using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Menues;

public class ContextMenuSubmenuItem : ContextMenuItem
{
    public ContextMenuSubmenuItem(string text, List<ContextMenuItem> submenu, bool enabled, bool round = true, double height = 34)
    {
        Text = text;
        Submenu = submenu;
        Enabled = enabled;
        Round = round;
        Height = height;
    }

    public string Text { get; set; } = "";
    public List<ContextMenuItem> Submenu { get; set; } = [];
    public bool Enabled { get; set; } = true;
    public bool Round { get; set; } = true;
    public double Height { get; set; } = 34;
    public Action? Clicked { get; set; }
    public Action? MouseEntered { get; set; }
    public Action? MouseLeft { get; set; }

    public override FrameworkElement GenerateElement(ContextMenuShell shell)
    {
        Grid g = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
        g.Children.Add(
            new TextBlock()
            {
                Text = Text.StartsWith("{") && Text.EndsWith("}") ? (Application.Current.FindResource(Text.TrimStart('{').TrimEnd('}')).ToString() ?? "") : Text,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            });
        g.Children.Add(
            new Path()
            {
                Data = Geometry.Parse("M310.6 233.4c12.5 12.5 12.5 32.8 0 45.3l-192 192c-12.5 12.5-32.8 12.5-45.3 0s-12.5-32.8 0-45.3L242.7 256 73.4 86.6c-12.5-12.5-12.5-32.8 0-45.3s32.8-12.5 45.3 0l192 192z"),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 12,
                Stretch = Stretch.Uniform,
                Fill = Brushes.White,
                Opacity = 0.4
            });

        Button b = new Button() { Content = g, HorizontalContentAlignment = HorizontalAlignment.Stretch, Style = Round ? Application.Current.FindResource("ContextMenuButton") as Style : Application.Current.FindResource("FlatButton") as Style, IsHitTestVisible = Enabled, Opacity = Enabled ? 1d : 0.4d, Height = Height };
        if (Enabled)
        {
            b.Click += (s, e) => Clicked?.Invoke();
            b.MouseEnter += (s, e) => MenuVisualizer.ShowMenu(Submenu, b, MenuSide.TopRight, space: 4, closeWhenMouseLeaves: true, padding: 4, tint: true, minWidth: 200);
        }
        return b;
    }
}