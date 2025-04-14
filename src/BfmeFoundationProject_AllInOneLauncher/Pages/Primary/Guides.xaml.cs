using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Primary;

public partial class Guides : UserControl
{
    internal static Guides Instance = new();

    public Guides()
    {
        InitializeComponent();
    }

    private void OnVideoClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo(((FrameworkElement)sender).Tag.ToString() ?? "") { UseShellExecute = true });
}