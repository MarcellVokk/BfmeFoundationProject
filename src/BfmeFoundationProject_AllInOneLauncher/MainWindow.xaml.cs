using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Pages.Primary;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Settings = BfmeFoundationProject.AllInOneLauncher.Properties.Settings;

namespace BfmeFoundationProject.AllInOneLauncher;

public partial class MainWindow : Window
{
    public static MainWindow? Instance { get; private set; }

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;

        LauncherStateManager.Init();
        LauncherUpdateManager.CheckForUpdates();

        TrayIcon.Visibility = Visibility.Collapsed;
        fullContent.Visibility = Visibility.Visible;

        Width = SystemParameters.WorkArea.Width * 0.72;
        Height = SystemParameters.WorkArea.Height * 0.85;

        foreach (BfmeGame game in Enum.GetValues(typeof(BfmeGame)).Cast<BfmeGame>().Where(g => g != BfmeGame.NONE))
        {
            if (BfmeRegistryManager.IsInstalled(game) && BfmeRegistryManager.GetKeyValue(game, BfmeRegistryKey.InstallPath).Contains(Path.GetDirectoryName(Environment.ProcessPath)!))
            {
                PopupVisualizer.ShowPopup(new MessagePopup("INVALID INSTALL LOCATION",
                        "The All In One Launcher has been installed inside one of the game's folders. This is not allowed, please reinstall the launcher in a different location!"),
                    OnPopupClosed: () =>
                    {
                        Application.Current.Shutdown();
                    });
                break;
            }
        }
        CheckSize();
        ReloadContextMenu();
        ShowOffline();

        Application.Current.Exit += OnApplicationExit;
        Loaded += (sender, e) => ProcessCommandLineArgs();

        BfmeWorkshopSyncManager.OnSyncBegin += (e) => OnSyncBegin();
        BfmeWorkshopSyncManager.OnSyncEnd += () => OnSyncEnd();
    }

    private static void ProcessCommandLineArgs()
    {
        if (App.Args.Length > 0)
        {
            if (App.Args[0] == "--LauncherChangelog")
                PopupVisualizer.ShowPopup(new LauncherChangelogPopup());
        }
    }

    public void OnSyncBegin()
    {
        Dispatcher.Invoke(() =>
        {
            Offline.Instance.Disabled = true;
            settingsIcon.IsHitTestVisible = false;
            settingsIcon.Opacity = 0.4;
        });
    }

    public void OnSyncEnd()
    {
        Dispatcher.Invoke(() =>
        {
            Offline.Instance.Disabled = false;
            settingsIcon.IsHitTestVisible = true;
            settingsIcon.Opacity = 1;
        });
    }

    public static void SetContent(FrameworkElement? newContent) => Instance!.content.Child = newContent;

    public static async void SetFullContent(FrameworkElement? newContent)
    {
        Instance!.content.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
        Instance.fullContent.Child = newContent;

        Instance.tabs.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;
        Instance.icons.Visibility = newContent != null ? Visibility.Collapsed : Visibility.Visible;

        Instance.background.Effect = newContent is Pages.Primary.Settings ? new BlurEffect() { Radius = 20 } : null;

        if (Pages.Primary.Settings.NeedsResync)
        {
            Pages.Primary.Settings.NeedsResync = false;

            foreach (BfmeGame game in Enum.GetValues(typeof(BfmeGame)).Cast<BfmeGame>().Where(g => g != BfmeGame.NONE))
            {
                if (!BfmeRegistryManager.IsInstalled(game) || (game == BfmeGame.ROTWK && !BfmeRegistryManager.IsInstalled(BfmeGame.BFME2)))
                    continue;

                if (await BfmeWorkshopManager.GetActivePatch((int)game) is BfmeWorkshopEntry activeEntry)
                    await BfmeSyncManager.SyncPackage($"{activeEntry.Guid}:{activeEntry.Version}");
            }
        }
    }


    public static void ShowOffline()
    {
        SetContent(Offline.Instance);

        foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
        {
            if (tab == Instance.offlineTab)
                tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
            else
            {
                tab.Foreground = Brushes.White;
                tab.Style = (Style)Instance.FindResource("TextBlockHover");
            }
        }
    }

    public static void ShowOnline()
    {
        SetContent(Online.Instance);

        foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
        {
            if (tab == Instance.onlineTab)
                tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
            else
            {
                tab.Foreground = Brushes.White;
                tab.Style = (Style)Instance.FindResource("TextBlockHover");
            }
        }
    }

    public static void ShowGuides()
    {
        SetContent(Guides.Instance);

        foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
        {
            if (tab == Instance.guidesTab)
                tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
            else
            {
                tab.Foreground = Brushes.White;
                tab.Style = (Style)Instance.FindResource("TextBlockHover");
            }
        }
    }

    public static void ShowAbout()
    {
        SetContent(About.Instance);

        foreach (TextBlock tab in Instance!.tabs.Children.OfType<TextBlock>())
        {
            if (tab == Instance.aboutTab)
                tab.Foreground = new SolidColorBrush(Color.FromRgb(21, 167, 233));
            else
            {
                tab.Foreground = Brushes.White;
                tab.Style = (Style)Instance.FindResource("TextBlockHover");
            }
        }
    }

    private void OnOfflineTabClicked(object sender, MouseButtonEventArgs e) => ShowOffline();
    private void OnOnlineTabClicked(object sender, MouseButtonEventArgs e) => ShowOnline();
    private void OnGuidesTabClicked(object sender, MouseButtonEventArgs e) => ShowGuides();
    private void OnAboutTabClicked(object sender, MouseButtonEventArgs e) => ShowAbout();

    private void OnSettingsButtonClicked(object sender, MouseButtonEventArgs e) => SetFullContent(new Pages.Primary.Settings("LauncherGeneral"));
    private void OnLinkButtonClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo() { FileName = ((FrameworkElement)sender).Tag.ToString() ?? "", UseShellExecute = true });

    private void OnLoad(object sender, RoutedEventArgs e) => CheckSize();
    private void OnSizeChanged(object sender, SizeChangedEventArgs e) => CheckSize();

    public void CheckSize() => windowGrid.LayoutTransform = new ScaleTransform(Math.Min(10, Math.Min((ActualWidth / 1700), (ActualHeight / 1200))), Math.Min(10, Math.Min((ActualWidth / 1700), (ActualHeight / 1200))));

    private void OnTrayIconDoubleClicked(object sender, RoutedEventArgs e) => LauncherStateManager.Visible = true;

    private void LauncherMainWindow_Closing(object sender, CancelEventArgs e)
    {
        if (Settings.Default.HideToTrayOnClose)
        {
            e.Cancel = true;
            ReloadContextMenu();
            TrayIcon.Visibility = Visibility.Visible;
            LauncherStateManager.Visible = false;
        }
        else
        {
            Application.Current.Shutdown();
        }
    }

    private void ReloadContextMenu()
    {
        if (TrayIcon.ContextMenu != null)
            TrayIcon.ContextMenu = null;

        ContextMenu newContextMenu = new()
        {
            Background = Brushes.White
        };
        TrayIcon.ContextMenu = newContextMenu;

        MenuItem showApplicationItem = new()
        {
            Header = Application.Current.FindResource("TrayIconShowApplication")
        };
        showApplicationItem.Click += (s, e) => LauncherStateManager.Visible = true;
        newContextMenu.Items.Add(showApplicationItem);

        MenuItem closeApplicationItem = new()
        {
            Header = Application.Current.FindResource("TrayIconCloseApplication")
        };
        closeApplicationItem.Click += (s, e) => Application.Current.Shutdown();
        newContextMenu.Items.Add(closeApplicationItem);
    }

    private void OnApplicationExit(object sender, ExitEventArgs e)
    {
        string appTempPath = Path.Combine(Path.GetTempPath(), Assembly.GetExecutingAssembly().GetName().Name!);
        if (Directory.Exists(appTempPath))
        {
            try
            {
                Directory.Delete(appTempPath, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting the folder with the name: {ex.Message}");
            }
        }
    }
}