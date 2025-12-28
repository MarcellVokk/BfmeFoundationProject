using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Native;

internal static class FolderPicker
{
    public static string? ShowDialog(Window owner, string title = "", string? initialPath = null)
    {
        var dlg = new OpenFolderDialog
        {
            Title = title,
            ValidateNames = true,
        };

        if (Directory.Exists(initialPath))
        {
            dlg.InitialDirectory = initialPath;
        }

        bool? result = dlg.ShowDialog(owner);
        if (result == true)
        {
            var selected = dlg.FolderName;
            if (Directory.Exists(selected))
                return selected;
        }

        return null;
    }
}
