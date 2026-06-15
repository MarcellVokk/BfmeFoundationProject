using System;
using System.IO;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Core.Utils;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using Microsoft.Win32;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class InstallGamePopup : PopupBody
{
    private string selectedLocation = "";

    public InstallGamePopup()
    {
        InitializeComponent();

        SetSelectedLocation(InstallLocationUtils.GetDefaultDriveRoot());
    }

    private void OnBrowseLocationClicked(object sender, RoutedEventArgs e)
    {
        OpenFolderDialog dialog = new()
        {
            Multiselect = false
        };

        if (Directory.Exists(selectedLocation))
            dialog.InitialDirectory = selectedLocation;
        else if (InstallLocationUtils.TryGetDrive(selectedLocation, out DriveInfo? drive) && drive != null)
            dialog.InitialDirectory = drive.RootDirectory.FullName;

        if (dialog.ShowDialog() == true)
            SetSelectedLocation(dialog.FolderName);
    }

    private void OnInstallClicked(object sender, RoutedEventArgs e)
    {
        if (InstallLocationUtils.IsValidLocation(selectedLocation))
            Submit(LanguageDropdown.SelectedValue, selectedLocation);
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();

    private void SetSelectedLocation(string location)
    {
        selectedLocation = InstallLocationUtils.NormalizeLocation(location);

        SelectedLocationText.Text = selectedLocation;
        SelectedLocationText.ToolTip = selectedLocation;

        ButtonAccept.IsEnabled = InstallLocationUtils.IsValidLocation(selectedLocation);
        UpdateDriveSpace();
        UpdateWarning();
    }

    private void UpdateDriveSpace()
    {
        SelectedLocationDriveFreeSpace.Text = "";

        if (!InstallLocationUtils.TryGetDrive(selectedLocation, out DriveInfo? drive) || drive == null)
            return;

        try
        {
            if (drive.IsReady)
                SelectedLocationDriveFreeSpace.Text = $"{Math.Floor(drive.AvailableFreeSpace / Math.Pow(1024, 3)):N0} GB {App.Current.FindResource("GenericFree")}";
        }
        catch
        {
            SelectedLocationDriveFreeSpace.Text = "";
        }
    }

    private void UpdateWarning()
    {
        bool showWarning = InstallLocationUtils.IsInProgramFilesFolder(selectedLocation);

        LocationWarningText.Text = showWarning ? App.Current.FindResource("InstallGamePopupProgramFilesWarning")?.ToString() ?? "" : "";
        LocationWarning.Visibility = showWarning ? Visibility.Visible : Visibility.Collapsed;
    }
}
