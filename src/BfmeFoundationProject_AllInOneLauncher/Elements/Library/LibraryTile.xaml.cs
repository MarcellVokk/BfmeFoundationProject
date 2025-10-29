using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Menues;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Settings = BfmeFoundationProject.AllInOneLauncher.Properties.Settings;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Library;

public partial class LibraryTile : UserControl
{
    public LibraryTile()
    {
        InitializeComponent();
        Settings.Default.SettingsSaving += (s, e) => UpdateType();

        BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
        BfmeWorkshopSyncManager.OnSyncUpdate += OnSyncUpdate;
        BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
    }

    BfmeWorkshopEntryPreview _workshopEntry;
    public BfmeWorkshopEntryPreview WorkshopEntry
    {
        get => _workshopEntry;
        set
        {
            _workshopEntry = value;
            title.Text = value.Name;
            version.Text = value.Version;
            author.Text = value.Author;

            IsHitTestVisible = BfmeRegistryManager.IsInstalled(value.Game);
            content.Opacity = IsHitTestVisible ? 1 : 0.5;

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

                    icon.Source = grayscaleArtwork;
                }
                catch { }
            }
            else
            {
                icon.Source = artwork;
            }

            UpdateType();
            Task.Run(UpdateIsActive);
        }
    }

    public bool IsLoading
    {
        get => loadingBar.Visibility == Visibility.Visible;
        set
        {
            if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
            {
                loadingBar.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                tags.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
            else
            {
                loadingBar.Visibility = Visibility.Hidden;
                tags.Visibility = Visibility.Visible;
            }

            loadingIcon.IsLoading = value;
            isActiveIcon.Visibility = value ? Visibility.Collapsed : Visibility.Visible;

            if (value)
                LoadProgress = 0;
        }
    }

    public double LoadProgress
    {
        get => (double)GetValue(LoadProgressProperty);
        set
        {
            SetValue(LoadProgressProperty, value);
            progressText.Text = $"{value}%";
        }
    }
    public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress", typeof(double), typeof(LibraryTile), new PropertyMetadata(OnLoadProgressChangedCallBack));
    private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        LibraryTile progressBar = (LibraryTile)sender;
        if (progressBar != null)
        {
            DoubleAnimation da = new() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
            progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
        }
    }

    private void OnLoad(object sender, RoutedEventArgs e) => Task.Run(CheckForUpdates);
    private void OnEnter(object sender, MouseEventArgs e) => hoverEffect.Opacity = 1;
    private void OnLeave(object sender, MouseEventArgs e) => hoverEffect.Opacity = 0;

    private void OnSyncBegin(BfmeWorkshopEntry entry)
    {
        Dispatcher.Invoke(() =>
        {
            if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
                IsLoading = entry.Guid == WorkshopEntry.Guid;
            else if (entry.Guid == WorkshopEntry.Guid)
                IsLoading = true;
        });
    }

    private void OnSyncUpdate(int progress, string status)
    {
        Dispatcher.Invoke(() =>
        {
            if (IsLoading && (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4))
                LoadProgress = progress;
        });
    }

    private void OnSyncEnd()
    {
        UpdateIsActive();
        Dispatcher.Invoke(() =>
        {
            IsLoading = false;

            IsHitTestVisible = BfmeRegistryManager.IsInstalled(WorkshopEntry.Game);
            content.Opacity = IsHitTestVisible ? 1 : 0.5;

            var artwork = new BitmapImage(new Uri(WorkshopEntry.ArtworkUrl));
            if (!IsHitTestVisible)
            {
                try
                {
                    var grayscaleArtwork = new FormatConvertedBitmap();
                    grayscaleArtwork.BeginInit();
                    grayscaleArtwork.Source = artwork;
                    grayscaleArtwork.DestinationFormat = PixelFormats.Gray32Float;
                    grayscaleArtwork.EndInit();

                    icon.Source = grayscaleArtwork;
                }
                catch { }
            }
            else
            {
                icon.Source = artwork;
            }
        });
    }

    private async void OnClicked(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            IsLoading = true;

            if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
            {
                await BfmeSyncManager.SyncPackage(WorkshopEntry.Guid);
            }
            else
            {
                if (isActiveIcon.Opacity == 0d)
                    await BfmeSyncManager.EnableEnhancement(WorkshopEntry.Guid);
                else
                    await BfmeSyncManager.DisableEnhancement(WorkshopEntry.Guid);
            }
        }
        else if (e.ChangedButton == MouseButton.Right)
        {
            if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
            {
                MenuVisualizer.ShowMenu(
                    menu: [
                        new ContextMenuButtonItem(isActiveIcon.Opacity == 0d ? $"Switch to \"{WorkshopEntry.Name}\"" : "Sync again", true, clicked: async () => await BfmeSyncManager.SyncPackage(WorkshopEntry.Guid)),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuSubmenuItem("Sync old version", submenu: WorkshopEntry.Metadata.Versions != null ? WorkshopEntry.Metadata.Versions.Where(x => x != WorkshopEntry.Version).Reverse<string>().Select(x => new ContextMenuButtonItem(x, true, clicked: async () => await BfmeSyncManager.SyncPackage($"{WorkshopEntry.Guid}:{x}")) as ContextMenuItem).ToList() : [], WorkshopEntry.Metadata.Versions != null && WorkshopEntry.Metadata.Versions.Count > 1),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Open workshop page", true, clicked: () => PopupVisualizer.ShowPopup(new PackagePagePopup() { WorkshopEntry = WorkshopEntry })),
                        new ContextMenuButtonItem("Open keybinds folder", true, clicked: () =>
                        {
                            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}")))
                                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}"));
                            Process.Start(new ProcessStartInfo("explorer.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Keybinds", $"{WorkshopEntry.GameName()}-{WorkshopEntry.Name}")));
                        }),
                        new ContextMenuButtonItem("Open game folder", true, clicked: () => Process.Start(new ProcessStartInfo("explorer.exe", Path.Combine(BfmeRegistryManager.GetKeyValue(WorkshopEntry.Game, BfmeRegistryKey.InstallPath))))),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Copy package GUID", true, clicked: () => Clipboard.SetDataObject(WorkshopEntry.Guid)),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Remove from library", true, clicked: RemoveFromLibrary)
                    ],
                    owner: this,
                    side: MenuSide.BottomRight,
                    padding: 4,
                    tint: true,
                    minWidth: 200,
                    targetCursor: true);
            }
            else
            {
                MenuVisualizer.ShowMenu(
                    menu: [
                        new ContextMenuButtonItem(isActiveIcon.Opacity == 0d ? $"Enable \"{WorkshopEntry.Name}\"" : "Disable", true, clicked: isActiveIcon.Opacity == 0d ? async () => await BfmeSyncManager.EnableEnhancement(WorkshopEntry.Guid) : async () => await BfmeSyncManager.DisableEnhancement(WorkshopEntry.Guid)),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuSubmenuItem("Sync old version", submenu: WorkshopEntry.Metadata.Versions != null ? WorkshopEntry.Metadata.Versions.Where(x => x != WorkshopEntry.Version).Reverse<string>().Select(x => new ContextMenuButtonItem(x, true, clicked: async () => await BfmeSyncManager.EnableEnhancement($"{WorkshopEntry.Guid}:{x}")) as ContextMenuItem).ToList() : [], WorkshopEntry.Metadata.Versions != null && WorkshopEntry.Metadata.Versions.Count > 1),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Open workshop page", true, clicked: () => PopupVisualizer.ShowPopup(new PackagePagePopup() { WorkshopEntry = WorkshopEntry })),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Copy package GUID", true, clicked: () => Clipboard.SetDataObject(WorkshopEntry.Guid)),
                        new ContextMenuSeparatorItem(),
                        new ContextMenuButtonItem("Remove from library", true, clicked: RemoveFromLibrary)
                    ],
                    owner: this,
                    side: MenuSide.BottomRight,
                    padding: 4,
                    tint: true,
                    minWidth: 200,
                    targetCursor: true);
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
        else if (WorkshopEntry.Type == 4)
            entryType.Text = Application.Current.FindResource("LibraryTileSnapshotType").ToString()!;
    }

    private void UpdateIsActive()
    {
        if (WorkshopEntry.Type == 0 || WorkshopEntry.Type == 1 || WorkshopEntry.Type == 4)
        {
            bool isActive = BfmeWorkshopManager.IsPatchActive(WorkshopEntry.Game, WorkshopEntry.Guid);
            Dispatcher.Invoke(() =>
            {
                activeText.Visibility = Visibility.Visible;
                isActiveIcon.Opacity = isActive ? 1d : 0d;
            });
        }
        else
        {
            bool isActive = BfmeWorkshopManager.IsEnhancementActive(WorkshopEntry.Game, WorkshopEntry.Guid);
            Dispatcher.Invoke(() =>
            {
                activeText.Visibility = Visibility.Collapsed;
                isActiveIcon.Opacity = isActive ? 1d : 0d;
            });
        }
    }

    private async void CheckForUpdates()
    {
        try
        {
            var latest = await BfmeWorkshopQueryManager.Get(WorkshopEntry.Guid);
            _workshopEntry.Metadata = latest.Metadata;
            Debug.WriteLine(latest.Size);
            if (WorkshopEntry.Version != latest.Version)
            {
                Dispatcher.Invoke(() => WorkshopEntry = latest);
                BfmeWorkshopLibraryManager.AddOrUpdate(await BfmeWorkshopDownloadManager.Download(latest.Guid));
            }
        }
        catch { }
    }

    private void RemoveFromLibrary()
    {
        BfmeWorkshopLibraryManager.Remove(WorkshopEntry.Guid);
        Pages.Primary.Offline.Instance.library.libraryTiles.Children.Remove(this);
    }
}