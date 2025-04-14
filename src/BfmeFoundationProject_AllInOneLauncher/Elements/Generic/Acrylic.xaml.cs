using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class Acrylic : UserControl
{
    public Acrylic()
    {
        InitializeComponent();
    }

    private void Update()
    {
        if (!IsLoaded || this.Background != null || this.FindCommonVisualAncestor(MainWindow.Instance!.windowGrid) == null)
            return;

        Point pos = this.TransformToVisual(MainWindow.Instance!.windowGrid).Transform(new Point(0, 0));
        var rsz = MainWindow.Instance!.windowGrid.TransformToDescendant(this).TransformBounds(new Rect(0, 0, MainWindow.Instance!.windowGrid.ActualWidth, MainWindow.Instance!.windowGrid.ActualHeight));
        image.Margin = new Thickness(-pos.X, -pos.Y, 0, 0);
        image.Width = rsz.Width;
        image.Height = rsz.Height;
    }

    private void OnLayoutUpdated(object sender, EventArgs e)
    {
        Update();
    }

    private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue == (object)true)
            Update();
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        if (DesignerProperties.GetIsInDesignMode(this))
            this.Background = Brushes.Gray;

        Update();
    }
}