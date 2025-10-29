using System;
using System.Windows;
using System.Windows.Controls;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Pages.Primary;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Settings.Launcher;

public partial class SettingsLauncherOnline : UserControl
{
    public SettingsLauncherOnline()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        UpdateBranchDropdown.SelectedValue = Properties.Settings.Default.ArenaUpdateBranch;
    }

    private void OnUpdateBranchOptionSelected(object sender, EventArgs e)
    {
        if (UpdateBranchDropdown.SelectedValue != Properties.Settings.Default.ArenaUpdateBranch)
        {
            Properties.Settings.Default.ArenaUpdateBranch = UpdateBranchDropdown.SelectedValue;
            Properties.Settings.Default.Save();

            Online.Instance.arena.UpdateBranch = UpdateBranchDropdown.SelectedValue;
            Online.Instance.Unload();
        }
    }
}