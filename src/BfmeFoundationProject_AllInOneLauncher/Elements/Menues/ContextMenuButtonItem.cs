using System;
using System.Windows;
using System.Windows.Controls;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Menues;

public class ContextMenuButtonItem : ContextMenuItem
{
    public ContextMenuButtonItem(string text, bool enabled, bool round = true, double height = 34, Action? clicked = null, Action? mouseEntered = null, Action? mouseLeft = null)
    {
        Text = text;
        Enabled = enabled;
        Round = round;
        Height = height;
        Clicked = clicked;
        MouseEntered = mouseEntered;
        MouseLeft = mouseLeft;
    }

    public string Text { get; set; } = "";
    public bool Enabled { get; set; } = true;
    public bool Round { get; set; } = true;
    public double Height { get; set; } = 34;
    public Action? Clicked { get; set; }
    public Action? MouseEntered { get; set; }
    public Action? MouseLeft { get; set; }

    public override FrameworkElement GenerateElement(ContextMenuShell shell)
    {
        Button b = new Button() { Content = Text.StartsWith("{") && Text.EndsWith("}") ? (Application.Current.FindResource(Text.TrimStart('{').TrimEnd('}')).ToString() ?? "") : Text, Style = Round ? Application.Current.FindResource("ContextMenuButton") as Style : Application.Current.FindResource("FlatButton") as Style, IsHitTestVisible = Enabled, Opacity = Enabled ? 1d : 0.4d, Height = Height };
        if (Enabled)
        {
            b.Click += (s, e) => Clicked?.Invoke();
            b.MouseEnter += (s, e) => shell.HideSubmenues();
        }
        return b;
    }
}