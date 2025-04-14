using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeKit;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Primary;

public partial class Settings : UserControl
{
    internal static bool NeedsResync;

    public Settings(string page)
    {
        InitializeComponent();
        Page = page;
        NeedsResync = false;

        foreach (var child in categoryTabs.Children)
        {
            if (child is Button button)
            {
                if (button.Tag.ToString()!.Contains('('))
                {
                    int gameValue = int.Parse(button.Tag.ToString()!.Split('(').Last().Split(')').First());
                    button.IsHitTestVisible = BfmeRegistryManager.IsInstalled(gameValue);
                }

                button.Opacity = button.IsHitTestVisible ? 1 : 0.4;
            }
        }
    }

    private string _page = "";
    public string Page
    {
        get => _page;
        set
        {
            _page = value;

            foreach (var child in categoryTabs.Children)
            {
                if (child is Button button)
                {
                    if (button.Tag.ToString() == value)
                        button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF"));
                    else
                        button.Background = Brushes.Transparent;
                }
            }

            string pageName = value.Split('(').First();
            int gameIndex = value.Contains('(') ? int.Parse(value.Split('(').Last().Split(')').First()) : -1;
            content.Child = gameIndex == -1 ? (FrameworkElement)Activator.CreateInstance(LauncherStateManager.TypeMap![$"Settings{pageName}"])! : (FrameworkElement)Activator.CreateInstance(LauncherStateManager.TypeMap![$"Settings{pageName}"], (BfmeGame)gameIndex)!;
        }
    }

    private void OnCloseClicked(object sender, MouseButtonEventArgs e) => MainWindow.SetFullContent(null);

    private void OnCategoryButtonClicked(object sender, RoutedEventArgs e) => Page = ((Button)sender).Tag.ToString()!;
}