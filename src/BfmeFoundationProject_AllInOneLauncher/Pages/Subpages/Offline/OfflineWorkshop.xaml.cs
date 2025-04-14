using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BfmeFoundationProject.AllInOneLauncher.Elements.Workshop;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Offline;

public partial class OfflineWorkshop : UserControl
{
    public OfflineWorkshop()
    {
        InitializeComponent();
        typeFilter.Options = ["{WorkshopPageFilterPatchesAndMods}", "{WorkshopPageFilterEnhancements}", "{WorkshopPageFilterMapPacks}", "{WorkshopPageFilterEverything}"];
        searchFilter.Options = ["{WorkshopPageSortByMostDownloads}", "{WorkshopPageSortByMostRecent}", "{WorkshopPageSortAlphabetical}"];
        typeFilter.Selected = 3;
    }

    private int Game = 0;

    private void OnReloadClicked(object sender, RoutedEventArgs e) => UpdateQuery();
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
        try
        {
            workshopContent.Visibility = Visibility.Visible;
            noConnection.Visibility = Visibility.Hidden;

            workshopTiles.Children.Clear();
            List<BfmeWorkshopEntryPreview> entries = await BfmeWorkshopQueryManager.Query(game: Game, keyword: search.Text, type: new[]{ -2, 2, 3, -1 }[typeFilter.Selected], sortMode: searchFilter.Selected);
            workshopTiles.Children.Clear();
            foreach (BfmeWorkshopEntryPreview entry in entries)
                workshopTiles.Children.Add(new WorkshopTile() { WorkshopEntry = entry, Margin = new Thickness(0, 0, 10, 10) });
        }
        catch
        {
            workshopContent.Visibility = Visibility.Hidden;
            noConnection.Visibility = Visibility.Visible;
        }
    }
}