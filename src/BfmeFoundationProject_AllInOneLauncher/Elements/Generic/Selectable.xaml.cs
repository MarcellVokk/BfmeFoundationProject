using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class Selectable : UserControl
{
    public static int GetSelectedIdInContainer(Panel container, string containerTag = "")
    {
        int id = 0;
        foreach(var selectable in container.Children.OfType<Selectable>())
        {
            if (containerTag != "" && selectable.ContainerTag != containerTag)
                continue;

            if (selectable.Selected)
                return id;

            id++;
        }

        foreach (var p in container.Children.OfType<Panel>())
        {
            id = GetSelectedIdInContainer(p, p.Tag.ToString() ?? "-");
            if (id != -1)
                return id;
        }

        return -1;
    }

    public static object? GetSelectedTagInContainer(Panel container, string containerTag = "")
    {
        if(container.Children.OfType<Selectable>().Any(x => x.Selected && (containerTag == "" ? true : (x.ContainerTag == containerTag))))
            return container.Children.OfType<Selectable>().First(x => x.Selected && (containerTag == "" ? true : (x.ContainerTag == containerTag))).Tag;

        object? tag = null;
        foreach (var p in container.Children.OfType<Panel>())
        {
            tag = GetSelectedTagInContainer(p, p.Tag.ToString() ?? "-");
            if (tag != null)
                return tag;
        }

        return null;
    }

    public Selectable()
    {
        InitializeComponent();
    }

    public event EventHandler? OnSelected;
    public event EventHandler? OnDeselected;

    public bool AllowMultiselect { get; set; } = false;
    public bool SelectDefault { get; set; } = true;

    public bool Selected
    {
        get => border_selected.Visibility == Visibility.Visible;
        set
        {
            border_selected.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            border_regular.Visibility = value ? Visibility.Collapsed : Visibility.Visible;

            if(!AllowMultiselect && value == true)
            {
                deselectAll(findParent(this));

                Panel findParent(FrameworkElement b)
                {
                    if (b.Parent is Panel && (ContainerTag == "" || (((Panel)b.Parent).Tag != null && ((Panel)b.Parent).Tag.ToString() == ContainerTag)))
                        return (Panel)b.Parent;
                    else if (b.Parent is FrameworkElement)
                        return findParent((FrameworkElement)b.Parent);
                    else
                        throw new Exception("Selectable: Parent not found.");
                }

                void deselectAll(Panel p)
                {
                    foreach (Selectable selectable in p.Children.OfType<Selectable>().Where(x => (ContainerTag == "" || x.ContainerTag == ContainerTag) && x != this))
                        selectable.Selected = false;

                    foreach (Panel pc in p.Children.OfType<Panel>())
                        deselectAll(pc);
                }
            }

            if (value)
                OnSelected?.Invoke(this, EventArgs.Empty);
            else
                OnDeselected?.Invoke(this, EventArgs.Empty);
        }
    }

    public object Title
    {
        get => (object)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(Selectable), new PropertyMetadata(null));

    public string ContainerTag { get; set; } = "";

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
            
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
            
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        if(AllowMultiselect)
            Selected = !Selected;
        else
            Selected = true;
    }

    private bool LoadedOnce = false;
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (LoadedOnce)
            return;

        LoadedOnce = true;

        Panel findParent(FrameworkElement b)
        {
            if (b.Parent is Panel && (ContainerTag == "" || (((Panel)b.Parent).Tag != null && ((Panel)b.Parent).Tag.ToString() == ContainerTag)))
                return (Panel)b.Parent;
            else if (b.Parent is FrameworkElement)
                return findParent((FrameworkElement)b.Parent);
            else
                throw new Exception("Selectable: Parent not found.");
        }

        bool anySelected(Panel p)
        {
            if (p.Children.OfType<Selectable>().Where(x => (ContainerTag == "" || x.ContainerTag == ContainerTag)).Any(x => x.Selected))
                return true;

            foreach (Panel pc in p.Children.OfType<Panel>())
                if (anySelected(pc))
                    return true;

            return false;
        }

        FrameworkElement? firstSelectable(Panel p)
        {
            if (p.Children.OfType<Selectable>().Any(x => (ContainerTag == "" || x.ContainerTag == ContainerTag)))
                return p.Children.OfType<Selectable>().First(x => (ContainerTag == "" || x.ContainerTag == ContainerTag));

            foreach (Panel pc in p.Children.OfType<Panel>())
            {
                var v = firstSelectable(pc);
                if(v != null)
                    return v;
            }

            return null;
        }

        if (SelectDefault && !AllowMultiselect)
        {
            Panel parent = findParent(this);

            if (!anySelected(parent) && firstSelectable(parent) == this)
                Selected = true;
        }
    }
}