using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Properties;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Offline;

public partial class EnabledEnhancementTile : UserControl
{
    public EnabledEnhancementTile()
    {
        InitializeComponent();
        Settings.Default.SettingsSaving += (s, e) => UpdateType();
    }

    BfmeWorkshopEntry _workshopEntry;
    public BfmeWorkshopEntry WorkshopEntry
    {
        get => _workshopEntry;
        set
        {
            _workshopEntry = value;

            activeEntryIcon.Source = null;
            activeEntryTitle.Text = value.Name;
            activeEntryVersion.Text = value.Version;
            activeEntryAuthor.Text = value.Author;
            UpdateType();

            IsHitTestVisible = BfmeRegistryManager.IsInstalled(value.Game);
            activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;

            var artwork = new BitmapImage(new Uri(value.ArtworkUrl));
            if (!IsHitTestVisible)
            {
                try
                {
                    var grayscaleArtwork = new FormatConvertedBitmap();
                    grayscaleArtwork.BeginInit();
                    grayscaleArtwork.Source = artwork;
                    grayscaleArtwork.DestinationFormat = PixelFormats.Gray32Float;
                    grayscaleArtwork.EndInit();

                    activeEntryIcon.Source = grayscaleArtwork;
                }
                catch { }
            }
            else
            {
                activeEntryIcon.Source = artwork;
            }
        }
    }

    private void UpdateType()
    {
        if (WorkshopEntry.Type == 0)
            entryType.Text = Application.Current.FindResource("LibraryTilePatchType").ToString()!;
        else if (WorkshopEntry.Type == 1)
            entryType.Text = Application.Current.FindResource("LibraryTileModType").ToString()!;
        else if (WorkshopEntry.Type == 2)
            entryType.Text = Application.Current.FindResource("LibraryTileEnhancementType").ToString()!;
        else if (WorkshopEntry.Type == 3)
            entryType.Text = Application.Current.FindResource("LibraryTileMapPackType").ToString()!;
    }

    private async void OnDisableClicked(object sender, RoutedEventArgs e)
    {
        await BfmeSyncManager.DisableEnhancement(WorkshopEntry.Guid);
    }
}