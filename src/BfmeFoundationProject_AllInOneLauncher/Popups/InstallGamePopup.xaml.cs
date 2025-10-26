using System.IO;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using BfmeFoundationProject.AllInOneLauncher.Elements.Disk;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Native;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class InstallGamePopup : PopupBody
{
    private static readonly List<DriveInfo> _drives = DriveUtils.GetValidDrives();
    private string _defaultPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

    public InstallGamePopup()
    {
        InitializeComponent();

        if (_drives.Count <= 0)
        {
            SelectLocationError.Visibility = Visibility.Visible;
            SelectLocationArea.Visibility = Visibility.Collapsed;
        }
        else
        {
            locations.Children.Clear();
            _drives.ForEach(drive =>
            {
                locations.Children.Add(new Selectable()
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

            SetSelectedPath(_defaultPath);
        }
    }

    private void SetSelectedPath(string path)
    {
        string? _selectedPath = null;
        var _selectedFreeText = string.Empty;
        try
        {
            _selectedPath = DriveUtils.GetValidPath(path);
            var drive = DriveUtils.GetDriveForPath(_drives, _selectedPath);
            if (drive == null)
            {
                _selectedPath = DriveUtils.GetValidPath(_defaultPath);
                drive = DriveUtils.GetDriveForPath(_drives, _selectedPath);
            }

            if (drive != null)
            {
                _selectedFreeText = GetDriveFreeSpaceFormatted(drive);
            }
        }
        catch { /* ignore errors here */ }

        SelectedPathText.Text = _selectedPath;
        SelectedFreeSpaceText.Text = _selectedFreeText;
        ButtonAccept.IsEnabled = !string.IsNullOrWhiteSpace(_selectedPath);
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
            var _selectedPath = SelectedPathText.Text;
            var selected = FolderPicker.ShowDialog(ownerWindow, folderDialogTitle, _selectedPath);
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
        var path = (SelectLocationAdvancedToggle.IsToggled == true) ? SelectedPathText.Text : Selectable.GetSelectedTagInContainer(locations)!.ToString()!;
        Submit(language, path);
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
}