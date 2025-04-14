using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Properties;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Library;

public partial class LibraryTileHorizontal : UserControl
{
    public LibraryTileHorizontal()
    {
        InitializeComponent();
        Settings.Default.SettingsSaving += (s, e) => UpdateType();

        _ = Task.Run(async () =>
        {
            while (true)
            {
                if (titleStack.ActualWidth <= availableTitleArea.ActualWidth)
                {
                    Dispatcher.Invoke(() =>
                    {
                        titleStack.BeginAnimation(MarginProperty, null);
                        titleStack.SetValue(MarginProperty, new Thickness(0));
                    });
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    continue;
                }

                var duration = TimeSpan.FromSeconds((titleStack.ActualWidth - availableTitleArea.ActualWidth) * 0.05);

                Dispatcher.Invoke(() =>
                {
                    ThicknessAnimation l = new() { To = new Thickness(availableTitleArea.ActualWidth - titleStack.ActualWidth, 0, 0, 0), Duration = duration };
                    Dispatcher.Invoke(() => titleStack.BeginAnimation(MarginProperty, l));
                });
                await Task.Delay(duration.Add(TimeSpan.FromSeconds(2)));

                if (titleStack.ActualWidth <= availableTitleArea.ActualWidth)
                {
                    Dispatcher.Invoke(() =>
                    {
                        titleStack.BeginAnimation(MarginProperty, null);
                        titleStack.SetValue(MarginProperty, new Thickness(0));
                    });
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    continue;
                }

                Dispatcher.Invoke(() =>
                {
                    ThicknessAnimation r = new() { To = new Thickness(0, 0, 0, 0), Duration = duration };
                    Dispatcher.Invoke(() => titleStack.BeginAnimation(MarginProperty, r));
                });
                await Task.Delay(duration.Add(TimeSpan.FromSeconds(2)));
            }
        });
    }

    BfmeWorkshopEntry? _workshopEntry = null;
    public BfmeWorkshopEntry? WorkshopEntry
    {
        get => _workshopEntry;
        set
        {
            _workshopEntry = value;

            if (value != null)
            {
                activeEntry.Visibility = Visibility.Visible;
                activeEntryNullIndicator.Visibility = Visibility.Hidden;

                activeEntryIcon.Source = null;
                activeEntryTitle.Text = value.Value.Name;
                activeEntryVersion.Text = value.Value.Version;
                activeEntryAuthor.Text = value.Value.Author;
                IsUpdateAvailable = false;
                UpdateType();
                Task.Run(CheckForUpdates);

                activeEntryLoading.Visibility = IsLoading ? Visibility.Visible : Visibility.Hidden;
                activeEntryActive.Visibility = IsLoading ? Visibility.Hidden : Visibility.Visible;
                activeEntryReloadButton.Visibility = IsLoading ? Visibility.Hidden : Visibility.Visible;

                IsHitTestVisible = BfmeRegistryManager.IsInstalled(value.Value.Game);
                activeEntry.Opacity = IsHitTestVisible ? 1 : 0.5;

                try
                {
                    var artwork = new BitmapImage(new Uri(value.Value.ArtworkUrl));
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
                catch { }
            }
            else
            {
                activeEntry.Visibility = Visibility.Hidden;
                activeEntryNullIndicator.Visibility = Visibility.Visible;
            }
        }
    }

    private bool isLoading = false;
    public bool IsLoading
    {
        get => isLoading;
        set
        {
            isLoading = value;

            activeEntryLoading.Visibility = value ? Visibility.Visible : Visibility.Hidden;
            loadingSpinner.IsLoading = value;
            activeEntryActive.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            activeEntryReloadButton.Visibility = value ? Visibility.Hidden : Visibility.Visible;
        }
    }

    public bool IsUpdateAvailable
    {
        get => updateText.Visibility == Visibility.Visible;
        set
        {
            updateText.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            updateIcon.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            syncAgainText.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            syncAgainIcon.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            activeEntryReloadButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString($"#{(value ? "FF5DAF47" : "19FFFFFF")}"));
        }
    }

    private async void OnResyncActiveEntry(object sender, RoutedEventArgs e)
    {
        if (WorkshopEntry is BfmeWorkshopEntry activeEntry)
        {
            IsUpdateAvailable = false;
            IsLoading = true;
            await BfmeSyncManager.SyncPackage(activeEntry.Guid, useFastFileCompare: false);
        }
    }

    private void UpdateType()
    {
        if (WorkshopEntry is BfmeWorkshopEntry activeEntry)
        {
            if (activeEntry.Type == 0)
                entryType.Text = Application.Current.FindResource("LibraryTilePatchType").ToString()!;
            else if (activeEntry.Type == 1)
                entryType.Text = Application.Current.FindResource("LibraryTileModType").ToString()!;
            else if (activeEntry.Type == 2)
                entryType.Text = Application.Current.FindResource("LibraryTileEnhancementType").ToString()!;
            else if (activeEntry.Type == 3)
                entryType.Text = Application.Current.FindResource("LibraryTileMapPackType").ToString()!;
            else if (activeEntry.Type == 4)
                entryType.Text = Application.Current.FindResource("LibraryTileSnapshotType").ToString()!;
        }
    }

    public async void CheckForUpdates()
    {
        try
        {
            if (WorkshopEntry is BfmeWorkshopEntry activeEntry)
            {
                var latest = await BfmeWorkshopQueryManager.Get(activeEntry.Guid);
                if (activeEntry.Guid == latest.Guid && activeEntry.Version != latest.Version)
                    Dispatcher.Invoke(() => IsUpdateAvailable = true);
            }
        }
        catch { }
    }
}