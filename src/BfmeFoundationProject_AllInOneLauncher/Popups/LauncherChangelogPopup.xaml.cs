using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class LauncherChangelogPopup : PopupBody
{
    public LauncherChangelogPopup()
    {
        InitializeComponent();
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
}