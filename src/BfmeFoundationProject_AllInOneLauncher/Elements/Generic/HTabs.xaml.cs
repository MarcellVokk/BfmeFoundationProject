using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class HTabs : UserControl
{
    private bool HasLoaded = false;

    public HTabs()
    {
        InitializeComponent();
    }

    public event EventHandler? SelectedIndexChanged;

    private List<ImageSource> _tabs = [];
    public List<ImageSource> Tabs
    {
        get => _tabs;
        set
        {
            _tabs = value;
            foreach (var tab in _tabs)
                tabs.Children.Add(new HTab() { Owner = this, Icon = tab });
        }
    }

    public int SelectedIndex
    {
        get => HasLoaded ? tabs.Children.OfType<HTab>().ToList().FindIndex(x => x.Selected) : InitialSelectedIndex;
        set
        {
            int i = 0;
            foreach (var tab in tabs.Children.OfType<HTab>())
            {
                tab.Selected = i == value;
                i++;
            }

            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public int InitialSelectedIndex { get; set; } = 0;

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (HasLoaded)
            return;

        HasLoaded = true;
        Tabs = _tabs;
        SelectedIndex = InitialSelectedIndex;
    }
}