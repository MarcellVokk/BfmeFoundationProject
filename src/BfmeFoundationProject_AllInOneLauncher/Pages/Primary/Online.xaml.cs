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
        arena.UpdateBranch = Properties.Settings.Default.ArenaUpdateBranch;
    }

    public void Unload()
    {
        arena.Unload();
        FirstLoad = true;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!FirstLoad)
            return;

        FirstLoad = false;
        arena.Load();
    }
}