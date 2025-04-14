using System.Windows;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Menues;

public abstract class ContextMenuItem
{
    public abstract FrameworkElement GenerateElement(ContextMenuShell shell);
}