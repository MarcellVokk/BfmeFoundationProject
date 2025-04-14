using System.Windows;
using System.Windows.Controls;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Primary;

public partial class Online : UserControl
{
    internal static Online Instance = new();
    private bool FirstLoad = true;

    public Online()
    {
        InitializeComponent();
    }

    public void Unload() => arena.Unload();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstLoad)
            return;

        FirstLoad = false;
        arena.Load();
    }
}