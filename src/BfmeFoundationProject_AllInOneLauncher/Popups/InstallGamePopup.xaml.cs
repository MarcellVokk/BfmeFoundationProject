using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using BfmeFoundationProject.AllInOneLauncher.Elements.Disk;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Elements.Native;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class InstallGamePopup : PopupBody
{
    private static readonly List<DriveInfo> _drives = DriveUtils.GetValidDrives();
    private string _defaultPath = string.Empty;

    public InstallGamePopup()
    {
        InitializeComponent();
        try
        {
            var firstReady = _drives.FirstOrDefault();
            if (firstReady != null)
            {
                _defaultPath = DriveUtils.GetDriveRootName(firstReady);
                SelectFolderError.Visibility = Visibility.Collapsed;
                SelectFolderButton.Visibility = Visibility.Visible;
                SetSelectedPath(_defaultPath);
            }
        }
        catch (Exception ex)
        {
            PopupVisualizer.ShowPopup(new ErrorPopup(ex), OnPopupClosed: () => Dismiss());
        }
    }

    private void SetSelectedPath(string path)
    {
        string _selectedPath; 
        var _selectedFreeText = string.Empty;
        try
        {
            _selectedPath = Path.GetFullPath(path); // validate path
            var drive = DriveUtils.GetDriveForPath(_drives, _selectedPath);
            if(drive == null) {
                _selectedPath = _defaultPath;
            }
        }
        catch { 
            _selectedPath = _defaultPath;
         }

        try
        {
            var drive = DriveUtils.GetDriveForPath(_drives, _selectedPath);
            if(drive != null) {
                _selectedFreeText = $"{Math.Floor(drive.AvailableFreeSpace / Math.Pow(1024, 3)):N0} GB {App.Current.FindResource("GenericFree")}";
            }
        }
        catch { /* ignore errors here */ }

        SelectedPathText.Text = _selectedPath;
        SelectedFreeText.Text = _selectedFreeText;
        ButtonAccept.IsEnabled = !string.IsNullOrWhiteSpace(_selectedPath);
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

    private void OnInstallClicked(object sender, RoutedEventArgs e) => Submit(LanguageDropdown.SelectedValue, SelectedPathText.Text);

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
}