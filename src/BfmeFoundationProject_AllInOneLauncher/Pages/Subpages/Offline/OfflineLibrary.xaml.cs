using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Library;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline;

public partial class OfflineLibrary : UserControl
{
    public OfflineLibrary()
    {
        InitializeComponent();
        filter.Options = ["{LibraryPageFilterPatchesAndMods}", "{LibraryPageFilterEnhancements}", "{LibraryPageFilterMapPacks}", "{LibraryPageFilterSnapshots}", "{LibraryPageFilterEverything}"];
        filter.Selected = 4;
    }

    private int Game = 0;

    private void OnInstallMoreClicked(object sender, MouseButtonEventArgs e) => Primary.Offline.Instance.ShowWorkshop();
    private void OnReloadClicked(object sender, RoutedEventArgs e)
    {
        UpdateQuery();
        Task.Run(Primary.Offline.Instance.activeEntry.CheckForUpdates);
    }
    private void OnFilterChanged(object sender, EventArgs e) => UpdateQuery();

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        searchPlaceholder.Visibility = search.Text == "" ? Visibility.Visible : Visibility.Hidden;
        UpdateQuery();
    }

    public void Load(int game)
    {
        Game = game;
        search.Text = "";

        UpdateQuery();
    }

    private async void UpdateQuery()
    {
        libraryTiles.Children.Clear();
        List<BfmeWorkshopEntryPreview> entries = await BfmeWorkshopLibraryManager.Query(game: Game, keyword: search.Text, type: new []{ -2, 2, 3, 4, -1 }[filter.Selected]);
        libraryTiles.Children.Clear();
        foreach (BfmeWorkshopEntryPreview entry in entries)
            libraryTiles.Children.Add(new LibraryTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        if (filter.Selected != 3) libraryTiles.Children.Add(emptyLibraryTile);
    }

    private async void OnCreateSnapshotClicked(object sender, RoutedEventArgs e)
    {
        Primary.Offline.Instance.Disabled = true;
        snapshotSpinner.IsLoading = true;
        snapshotIcon.Visibility = Visibility.Hidden;

        try
        {
            var entry = await BfmeWorkshopSyncManager.CreateSnapshot(Game);
            BfmeWorkshopLibraryManager.AddOrUpdate(entry);
            if (filter.Selected == 2) UpdateQuery();
        }
        catch(Exception ex)
        {
            PopupVisualizer.ShowPopup(new ErrorPopup(ex));
        }
        finally
        {
            snapshotSpinner.IsLoading = false;
            snapshotIcon.Visibility = Visibility.Visible;
            Primary.Offline.Instance.Disabled = false;
        }
    }
}