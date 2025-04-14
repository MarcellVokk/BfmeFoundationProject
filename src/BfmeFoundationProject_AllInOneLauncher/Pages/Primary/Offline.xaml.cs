using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Offline;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Primary;

public partial class Offline : UserControl
{
    public static Offline Instance = new Offline();

    private int previousSelectedIndex = -1;

    public Offline()
    {
        InitializeComponent();

        Properties.Settings.Default.SettingsSaving += (s, e) =>
        {
            UpdateTitleImage();
            UpdatePlayButton();
        };

        BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
        BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
    }

    private void OnLibraryTabClicked(object sender, MouseButtonEventArgs e) => ShowLibrary();
    private void OnWorkshopTabClicked(object sender, MouseButtonEventArgs e) => ShowWorkshop();

    private void OnSyncBegin(BfmeWorkshopEntry entry)
    {
        Dispatcher.Invoke(() =>
        {
            if (entry.Game == gameTabs.SelectedIndex && (entry.Type == 0 || entry.Type == 1 || entry.Type == 4))
            {
                activeEntry.WorkshopEntry = entry;
                activeEntry.IsLoading = true;
            }
        });
    }

    private void OnSyncEnd()
    {
        Dispatcher.Invoke(() =>
        {
            activeEntry.IsLoading = false;
            UpdatePlayButton();
            UpdateEnabledEnhancements();
        });
    }

    public bool Disabled
    {
        get => gameTabs.IsHitTestVisible == false;
        set
        {
            gameTabs.IsHitTestVisible = !value;
            innerTabs.IsHitTestVisible = !value;
            library.IsHitTestVisible = !value;
            enabledEnhancements.IsHitTestVisible = !value;
            activeEntry.IsHitTestVisible = !value;
        }
    }

    public void ShowLibrary()
    {
        foreach (Border tab in Instance!.innerTabs.Children.OfType<Border>())
        {
            if (tab == libraryTab)
                tab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1EFFFFFF"));
            else
                tab.Background = Brushes.Transparent;
        }

        library.Visibility = Visibility.Visible;
        workshop.Visibility = Visibility.Hidden;

        library.Load(gameTabs.SelectedIndex);
    }

    public void ShowWorkshop()
    {
        foreach (Border tab in Instance!.innerTabs.Children.OfType<Border>())
        {
            if (tab == workshopTab)
                tab.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1EFFFFFF"));
            else
                tab.Background = Brushes.Transparent;
        }

        library.Visibility = Visibility.Hidden;
        workshop.Visibility = Visibility.Visible;

        workshop.Load(gameTabs.SelectedIndex);
    }

    private void OnLaunchGameClicked(object sender, EventArgs e)
    {
        BfmeLaunchManager.LaunchGame((BfmeGame)gameTabs.SelectedIndex);
    }

    private void OnInstallGameClicked(object sender, EventArgs e)
    {
        PopupVisualizer.ShowPopup(new InstallGamePopup(),
            OnPopupSubmited: async (submittedData) => await BfmeSyncManager.InstallGame((BfmeGame)gameTabs.SelectedIndex, submittedData[0], submittedData[1]));
    }

    private async void TabChanged(object sender, EventArgs e)
    {
        if (gameTabs.SelectedIndex != previousSelectedIndex)
        {
            previousSelectedIndex = gameTabs.SelectedIndex;
            activeEntry.WorkshopEntry = await BfmeWorkshopManager.GetActivePatch(previousSelectedIndex);

            UpdateTitleImage();
            UpdatePlayButton();
            UpdateEnabledEnhancements();

            if (library.Visibility == Visibility.Visible)
                ShowLibrary();
            else if (workshop.Visibility == Visibility.Visible)
                ShowWorkshop();
        }
    }

    private void UpdateTitleImage()
    {
        string game;
        if (gameTabs.SelectedIndex == 0)
            game = "BFME1";
        else if (gameTabs.SelectedIndex == 1)
            game = "BFME2";
        else if (gameTabs.SelectedIndex == 2)
            game = "Rotwk";
        else
            return;

        string language = "en";
        if (LauncherStateManager.Language == 1)
            language = "de";

        titleImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Resources/Images/{language}_{game}_title.png"));
    }

    private void UpdatePlayButton()
    {
        if (BfmeRegistryManager.IsInstalled(gameTabs.SelectedIndex))
            launchButton.ButtonState = LaunchButtonState.Launch;
        else
            launchButton.ButtonState = LaunchButtonState.Install;
    }

    private async void UpdateEnabledEnhancements()
    {
        enabledEnhancements.Children.Clear();
        foreach (BfmeWorkshopEntry entry in (await BfmeWorkshopManager.GetActiveEnhancements(gameTabs.SelectedIndex)).Values)
            enabledEnhancements.Children.Add(new EnabledEnhancementTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 0, 10) });
        activeEnhancementsNullIndicator.Visibility = enabledEnhancements.Children.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
    }
}