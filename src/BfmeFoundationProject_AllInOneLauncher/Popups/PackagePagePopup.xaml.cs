using System;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.BfmeKit;
using System.Globalization;
using BfmeFoundationProject.AllInOneLauncher.Elements;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class PackagePagePopup : PopupBody
{
    public override ColorStyle ColorStyle => ColorStyle.Acrylic;

    public PackagePagePopup()
    {
        InitializeComponent();
    }

    BfmeWorkshopEntryPreview _workshopEntry;
    public BfmeWorkshopEntryPreview WorkshopEntry
    {
        get => _workshopEntry;
        set
        {
            _workshopEntry = value;
            try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
            screenshots.Screenshots = value.ScreenshotUrls.Count == 0 ? ["", ""] : value.ScreenshotUrls;
            title.Text = value.Name;
            author.Text = value.Author;
            description.Text = value.Description;
            downloads.Text = NumberUtils.IntAsPrettyNumber(value.Metadata.Downloads);
            game.Text = value.GameName();
            IsInLibrary = BfmeWorkshopLibraryManager.IsInLibrary(value.Guid);

            icon_bfme1.Visibility = value.Game == 0 ? Visibility.Visible : Visibility.Collapsed;
            icon_bfme2.Visibility = value.Game == 1 ? Visibility.Visible : Visibility.Collapsed;
            icon_rotwk.Visibility = value.Game == 2 ? Visibility.Visible : Visibility.Collapsed;

            if (value.Type == 0)
                type.Text = Application.Current.FindResource("LibraryTilePatchType").ToString()!;
            else if (value.Type == 1)
                type.Text = Application.Current.FindResource("LibraryTileModType").ToString()!;
            else if (value.Type == 2)
                type.Text = Application.Current.FindResource("LibraryTileEnhancementType").ToString()!;
            else if (value.Type == 3)
                type.Text = Application.Current.FindResource("LibraryTileMapPackType").ToString()!;

            icon_patch.Visibility = value.Type == 0 ? Visibility.Visible : Visibility.Collapsed;
            icon_mod.Visibility = value.Type == 1 ? Visibility.Visible : Visibility.Collapsed;
            icon_enhancement.Visibility = value.Type == 2 ? Visibility.Visible : Visibility.Collapsed;
            icon_mappack.Visibility = value.Type == 3 ? Visibility.Visible : Visibility.Collapsed;

            value.Language = value.Language.Contains("EN") ? value.Language : (value.Language + " EN").TrimStart(' ');
            var curentGameLanguage = BfmeRegistryManager.GameLanguageToLanguageCode(BfmeRegistryManager.GetKeyValue(value.Game, BfmeFoundationProject.BfmeKit.Data.BfmeRegistryKey.Language));
            language_code.Text = value.Language.Contains(curentGameLanguage) ? curentGameLanguage : value.Language.Split(' ')[0];
            language_full.Text = value.Language.Split(' ').Length > 1 ? $"+ {value.Language.Split(' ').Length - 1} More" : BfmeRegistryManager.GameLanguageCodeToLanguage(language_code.Text);

            if (value.Size >= 1_073_741_824)
            {
                size_unit.Text = "GB";
                size_number.Text = Math.Round(value.Size / 1_073_741_824d, 2).ToString(CultureInfo.InvariantCulture).Replace(".", ",");
            }
            else if (value.Size >= 1_048_576)
            {
                size_unit.Text = "MB";
                size_number.Text = Math.Round(value.Size / 1_048_576d, 2).ToString(CultureInfo.InvariantCulture).Replace(".", ",");
            }
            else
            {
                size_unit.Text = "KB";
                size_number.Text = Math.Round(value.Size / 1024d, 2).ToString(CultureInfo.InvariantCulture).Replace(".", ",");
            }

            changelog.Children.Add(new PackagePageChangelogItem() { Version = value.Version, CreationTime = value.CreationTime, Text = value.Changelog });
        }
    }

    private bool isLoading = false;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;
            mainButtonLoadingSpinner.IsLoading = value;
            mainButtonText.Visibility = value ? Visibility.Hidden : Visibility.Visible;
        }
    }

    public bool IsInLibrary
    {
        get => mainButtonAlreadyInLibraryText.Visibility == Visibility.Visible;
        set
        {
            mainButtonAddToLibraryText.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            mainButtonAlreadyInLibraryText.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            //mainButton.Opacity = value ? 0.4 : 1;
            mainButton.IsHitTestVisible = value ? false : true;
        }
    }

    private async void AddToLibrary()
    {
        try
        {
            IsLoading = true;

            var entry = await BfmeWorkshopDownloadManager.Download(WorkshopEntry.Guid);

            BfmeWorkshopLibraryManager.AddOrUpdate(entry);
            IsInLibrary = true;

            IsLoading = false;
        }
        catch (Exception ex)
        {
            PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            Dismiss();
        }

        IsLoading = false;
    }

    private void OnMainButtonClicked(object sender, RoutedEventArgs e)
    {
        if (IsLoading)
            return;

        AddToLibrary();
    }

    private void OnDismissClicked(object sender, RoutedEventArgs e) => Dismiss();
}