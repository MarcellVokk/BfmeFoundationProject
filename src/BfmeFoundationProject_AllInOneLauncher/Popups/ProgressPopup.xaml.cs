using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class ProgressPopup : PopupBody
{
    public ProgressPopup(string title, string message)
    {
        InitializeComponent();
        this.title.Text = string.Join("", title.Split("{").Select(x => !x.Contains("}") ? x : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));
        this.message.Text = string.Join("", message.Split("{").Select(x => !x.Contains("}") ? x : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));

        LoadProgress = 0;
    }

    public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress", typeof(double), typeof(ProgressPopup), new PropertyMetadata(OnLoadProgressChangedCallBack));
    public double LoadProgress
    {
        get => (double)GetValue(LoadProgressProperty);
        set
        {
            SetValue(LoadProgressProperty, value);
            progressText.Text = $"{value}%";
        }
    }

    public string Status
    {
        get => status.Text;
        set => status.Text = value;
    }

    private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        var progressBar = (ProgressPopup)sender;
        if (progressBar != null)
        {
            var da = new DoubleAnimation() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
            progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
        }
    }
}