using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BfmeFoundationProject.AllInOneLauncher.Elements.Menues;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class MenuVisualizer : UserControl
{
    private static MenuVisualizer? Instance;
    public static List<ContextMenuShell> ActiveMenues = new List<ContextMenuShell>();

    public MenuVisualizer()
    {
        InitializeComponent();
        Instance = this;
    }

    public static Brush StandardBrush => new SolidColorBrush(Color.FromArgb(255, 37, 37, 38));
    public static Rect GetAbsolutePlacement(FrameworkElement element) => new Rect(element.TransformToVisual(Instance!.content).Transform(new Point(0, 0)), new Size(element.ActualWidth, element.ActualHeight));

    public static ContextMenuShell? ShowMenu(string text, FrameworkElement owner, MenuSide side, double space = 2, double padding = 0, bool tint = false, CornerRadius? corners = null, ColorStyle colorStyle = ColorStyle.Acrylic, bool fullWidth = false, double minWidth = 0, double lifetime = -1, bool closeWhenMouseLeaves = false, bool targetCursor = false, Action? onDestroy = null)
    {
        if (Instance == null)
            return null;

        if (ActiveMenues.Any(x => x.Owner == owner))
            return null;

        ContextMenuShell menuShell = new ContextMenuShell(owner, side, corners ?? new CornerRadius(5), colorStyle, fullWidth, minWidth, lifetime, closeWhenMouseLeaves, padding, tint, onDestroy, (menu, menuWidth, menuHeight) =>
            {
                Rect ownerRect = GetAbsolutePlacement(owner);
                Point finalPos = new Point(ownerRect.X, ownerRect.Y);

                if (side == MenuSide.Left)
                {
                    finalPos.X = ownerRect.X - menuWidth - space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Right)
                {
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Top)
                {
                    finalPos.Y = ownerRect.Y - menuHeight - space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.Bottom)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.TopLeft)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X - menuWidth - space;
                }
                else if (side == MenuSide.TopRight)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }
                else if (side == MenuSide.BottomRight)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }

                if (!targetCursor)
                {
                    if (side == MenuSide.TopRight && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.TopLeft;
                        finalPos.Y = ownerRect.Y - padding;
                        finalPos.X = ownerRect.X - menuWidth - space;
                    }

                    if (side == MenuSide.Right && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.Left;
                        finalPos.X = ownerRect.X - menuWidth - space;
                        finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                    }

                    if (side == MenuSide.Bottom && finalPos.Y + menuHeight > Instance.content.ActualHeight)
                    {
                        menu.Side = MenuSide.Top;
                        finalPos.Y = ownerRect.Y - menuHeight - space;
                        finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                    }
                }
                else
                {
                    finalPos = Mouse.GetPosition(Instance);
                }

                menu.Margin = new Thickness(finalPos.X, finalPos.Y, 0, 0);
                menu.IsMenuVisible = true;
            })
            { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, ContentText = text, ContentType = MenuContent.Text };

        Instance.content.Children.Add(menuShell);
        ActiveMenues.Add(menuShell);

        owner.IsVisibleChanged += (s, e) =>
        {
            if (e.NewValue == (object)false)
                HideMenu(menuShell);
        };

        owner.Unloaded += (s, e) => HideMenu(menuShell);

        return menuShell;
    }

    public static ContextMenuShell? ShowMenu(Page content, FrameworkElement owner, MenuSide side, double space = 2, double padding = 0, bool tint = false, CornerRadius? corners = null, ColorStyle colorStyle = ColorStyle.Acrylic, bool fullWidth = false, double minWidth = 0, double lifetime = -1, bool closeWhenMouseLeaves = false, bool targetCursor = false, Action? onDestroy = null)
    {
        if (Instance == null)
            return null;

        if (ActiveMenues.Any(x => x.Owner == owner))
            return null;

        ContextMenuShell menuShell = new ContextMenuShell(owner, side, corners ?? new CornerRadius(5), colorStyle, fullWidth, minWidth, lifetime, closeWhenMouseLeaves, padding, tint, onDestroy, (menu, menuWidth, menuHeight) =>
            {
                Rect ownerRect = GetAbsolutePlacement(owner);
                Point finalPos = new Point(ownerRect.X, ownerRect.Y);

                if (side == MenuSide.Left)
                {
                    finalPos.X = ownerRect.X - menuWidth - space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Right)
                {
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Top)
                {
                    finalPos.Y = ownerRect.Y - menuHeight - space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.Bottom)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.TopLeft)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X - menuWidth - space;
                }
                else if (side == MenuSide.TopRight)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }
                else if (side == MenuSide.BottomRight)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }

                if (!targetCursor)
                {
                    if (side == MenuSide.TopRight && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.TopLeft;
                        finalPos.Y = ownerRect.Y - padding;
                        finalPos.X = ownerRect.X - menuWidth - space;
                    }

                    if (side == MenuSide.Right && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.Left;
                        finalPos.X = ownerRect.X - menuWidth - space;
                        finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                    }

                    if (side == MenuSide.Bottom && finalPos.Y + menuHeight > Instance.content.ActualHeight)
                    {
                        menu.Side = MenuSide.Top;
                        finalPos.Y = ownerRect.Y - menuHeight - space;
                        finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                    }
                }
                else
                {
                    finalPos = Mouse.GetPosition(Instance);
                }

                menu.Margin = new Thickness(finalPos.X, finalPos.Y, 0, 0);
                menu.IsMenuVisible = true;
            })
            { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, ContentPage = content, ContentType = MenuContent.Page };

        Instance.content.Children.Add(menuShell);
        ActiveMenues.Add(menuShell);

        owner.IsVisibleChanged += (s, e) =>
        {
            if (e.NewValue == (object)false)
                HideMenu(menuShell);
        };

        owner.Unloaded += (s, e) => HideMenu(menuShell);

        return menuShell;
    }

    public static ContextMenuShell? ShowMenu(List<ContextMenuItem> menu, FrameworkElement owner, MenuSide side, double space = 2, double padding = 0, bool tint = false, CornerRadius? corners = null, ColorStyle colorStyle = ColorStyle.Acrylic, bool fullWidth = false, double minWidth = 0, double lifetime = -1, bool closeWhenMouseLeaves = false, bool targetCursor = false, Action? onDestroy = null)
    {
        if (Instance == null)
            return null;

        if (ActiveMenues.Any(x => x.Owner == owner))
            return null;

        ContextMenuShell menuShell = new ContextMenuShell(owner, side, corners ?? new CornerRadius(5), colorStyle, fullWidth, minWidth, lifetime, closeWhenMouseLeaves, padding, tint, onDestroy, (menu, menuWidth, menuHeight) =>
            {
                Rect ownerRect = GetAbsolutePlacement(owner);
                Point finalPos = new Point(ownerRect.X, ownerRect.Y);

                if (side == MenuSide.Left)
                {
                    finalPos.X = ownerRect.X - menuWidth - space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Right)
                {
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                    finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                }
                else if (side == MenuSide.Top)
                {
                    finalPos.Y = ownerRect.Y - menuHeight - space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.Bottom)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                }
                else if (side == MenuSide.TopLeft)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X - menuWidth - space;
                }
                else if (side == MenuSide.TopRight)
                {
                    finalPos.Y = ownerRect.Y - padding;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }
                else if (side == MenuSide.BottomRight)
                {
                    finalPos.Y = ownerRect.Y + ownerRect.Height + space;
                    finalPos.X = ownerRect.X + ownerRect.Width + space;
                }

                if (!targetCursor)
                {
                    if (side == MenuSide.TopRight && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.TopLeft;
                        finalPos.Y = ownerRect.Y - padding;
                        finalPos.X = ownerRect.X - menuWidth - space;
                    }

                    if (side == MenuSide.Right && finalPos.X + menuWidth > Instance.content.ActualWidth)
                    {
                        menu.Side = MenuSide.Left;
                        finalPos.X = ownerRect.X - menuWidth - space;
                        finalPos.Y = ownerRect.Y - menuHeight / 2 + ownerRect.Height / 2;
                    }

                    if (side == MenuSide.Bottom && finalPos.Y + menuHeight > Instance.content.ActualHeight)
                    {
                        menu.Side = MenuSide.Top;
                        finalPos.Y = ownerRect.Y - menuHeight - space;
                        finalPos.X = ownerRect.X - menuWidth / 2 + ownerRect.Width / 2;
                    }
                }
                else
                {
                    finalPos = Mouse.GetPosition(Instance);
                }

                menu.Margin = new Thickness(finalPos.X, finalPos.Y, 0, 0);
                menu.IsMenuVisible = true;
            })
            { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, ContentMenu = menu, ContentType = MenuContent.Menu };

        Instance.content.Children.Add(menuShell);
        ActiveMenues.Add(menuShell);

        owner.IsVisibleChanged += (s, e) =>
        {
            if (e.NewValue == (object)false)
                HideMenu(menuShell);
        };

        owner.Unloaded += (s, e) => HideMenu(menuShell);

        return menuShell;
    }

    public static void HideMenu(ContextMenuShell popup)
    {
        if (Instance == null)
            return;

        popup.StartDestroy();
    }

    public static void HideMenuOn(FrameworkElement owner, MenuContent? ofMenuType = null)
    {
        if (Instance == null)
            return;

        ActiveMenues.Where(x => x.Owner == owner && (ofMenuType == null || x.ContentType == ofMenuType.Value)).ToList().ForEach(x => x.StartDestroy());
    }

    public static void DestroyMenu(ContextMenuShell menu)
    {
        if (Instance == null)
            return;

        ActiveMenues.Remove(menu);
        Instance.content.Children.Remove(menu);
    }

    public static void HideAllMenues()
    {
        if (Instance == null)
            return;

        ActiveMenues.Clear();
        Instance.content.Children.OfType<ContextMenuShell>().ToList().ForEach(x => x.StartDestroy());
    }

    public static void DestroyAllMenues()
    {
        if (Instance == null)
            return;

        ActiveMenues.Clear();
        Instance.content.Children.Clear();
    }

    public static bool HasMenuOn(FrameworkElement owner)
    {
        if (Instance == null)
            return false;

        return Instance.content.Children.OfType<ContextMenuShell>().Any(x => x.Owner == owner);
    }

    private void OnResized(object sender, SizeChangedEventArgs e)
    {
        foreach (ContextMenuShell menu in ActiveMenues)
            menu.UpdatePos();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        getParent(this);

        void getParent(FrameworkElement child)
        {
            if (child.Parent is Window)
                ((Window)child.Parent).PreviewMouseUp += (s, e) => HideAllMenues();
            else if (child.Parent is FrameworkElement)
                getParent((FrameworkElement)child.Parent);
        }
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        foreach (ContextMenuShell menu in ActiveMenues.Where(x => x.CloseWhenMouseLeaves).ToList())
            menu.StartDestroy();
    }
}

public enum MenuSide
{
    Left,
    Right,
    Top,
    Bottom,
    TopLeft,
    TopRight,
    BottomRight,
}

public enum MenuContent
{
    Text,
    Page,
    Menu
}