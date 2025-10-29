using System.IO;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using BfmeFoundationProject.AllInOneLauncher.Elements.Disk;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Native;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class InstallGamePopup : PopupBody
{
    private readonly List<DriveInfo> Drives = DriveUtils.GetValidDrives();
    private readonly string DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

    public InstallGamePopup()
    {
        InitializeComponent();

        if (Drives.Count <= 0)
        {
            SelectLocationError.Visibility = Visibility.Visible;
            SelectLocationArea.Visibility = Visibility.Collapsed;
        }
        else
        {
            Locations.Children.Clear();
            Drives.ForEach(drive =>
            {
                Locations.Children.Add(new Selectable()
                {
                    Title = new LibraryDriveHeader()
                    {
                        LibraryDriveName = string.Concat(drive.VolumeLabel, " (", drive.Name.Replace(@"\", ""), ")"),
                        LibraryDriveSize = GetDriveFreeSpaceFormatted(drive),
                        Mini = true
                    },
                    Tag = DriveUtils.GetDriveRootName(drive),
                    Margin = new Thickness(0, 0, 0, 5),
                    UseLayoutRounding = true,
                    SnapsToDevicePixels = true
                });

            });

            SetSelectedPath(DefaultPath);
        }
    }

    private void SetSelectedPath(string path)
    {
        string? selectedPath = null;
        var selectedFreeText = string.Empty;
        try
        {
            selectedPath = DriveUtils.GetValidPath(path);
            var drive = DriveUtils.GetDriveForPath(Drives, selectedPath);
            if (drive == null)
            {
                selectedPath = DriveUtils.GetValidPath(DefaultPath);
                drive = DriveUtils.GetDriveForPath(Drives, selectedPath);
            }

            if (drive != null)
            {
                selectedFreeText = GetDriveFreeSpaceFormatted(drive);
            }
        }
        catch { /* ignore errors here */ }

        SelectedPathText.Text = selectedPath;
        SelectedFreeSpaceText.Text = selectedFreeText;
        ButtonAccept.IsEnabled = !string.IsNullOrWhiteSpace(selectedPath);
    }

    private static string GetDriveFreeSpaceFormatted(DriveInfo drive)
    {
        return $"{Math.Floor(drive.AvailableFreeSpace / Math.Pow(1024, 3)):N0} GB {App.Current.FindResource("GenericFree")}";
    }

    private void OnSelectFolderClicked(object sender, RoutedEventArgs e)
    {
        try
        {
            var ownerWindow = Window.GetWindow(this);
            var folderDialogTitle = (string)App.Current.FindResource("InstallGamePopupSelectFolder");
            var selectedPath = SelectedPathText.Text;
            var selected = FolderPicker.ShowDialog(ownerWindow, folderDialogTitle, selectedPath);
            if (!string.IsNullOrWhiteSpace(selected))
                SetSelectedPath(selected);
        }
        catch (Exception ex)
        {
            PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            Dismiss();
        }
    }

    private void OnAdvancedSettingsSwitched(object sender, EventArgs e)
    {
        var isActive = SelectLocationAdvancedToggle.IsToggled;
        SelectLocationList.Visibility = isActive ? Visibility.Collapsed : Visibility.Visible;
        SelectFolderButton.Visibility = isActive ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnInstallClicked(object sender, RoutedEventArgs e)
    {
        var language = LanguageDropdown.SelectedValue;
        var path = (SelectLocationAdvancedToggle.IsToggled == true) ? SelectedPathText.Text : Selectable.GetSelectedTagInContainer(Locations)!.ToString()!;
        Submit(language, path);
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
}