using System;
using System.Windows;
using System.Windows.Controls;
using BfmeFoundationProject.AllInOneLauncher.Core;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Settings.Launcher;

public partial class SettingsLauncherGeneral : UserControl
{
    public SettingsLauncherGeneral()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        LanguageDropdown.Selected = Properties.Settings.Default.LauncherLanguage;
        CloseToTrayToggle.IsToggled = Properties.Settings.Default.HideToTrayOnClose;
    }

    private void OnLanguageOptionSelected(object sender, EventArgs e)
    {
        LauncherStateManager.Language = LanguageDropdown.Selected;
    }

    private void OnCloseToTraySwitched(object sender, EventArgs e)
    {
        Properties.Settings.Default.HideToTrayOnClose = CloseToTrayToggle.IsToggled;
        Properties.Settings.Default.Save();
    }
}