using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class LauncherUpdatePopup : PopupBody
{
    public LauncherUpdatePopup()
    {
        InitializeComponent();
        title.Text = File.Exists(Path.Combine(LauncherUpdateManager.LauncherAppDirectory, "AllInOneLauncher.exe")) ? "UPDATING" : "INSTALLING";
        LoadProgress = 0;
    }

    public double LoadProgress
    {
        get => (double)GetValue(LoadProgressProperty);
        set
        {
            SetValue(LoadProgressProperty, value);
            progressText.Text = $"{value}%";
        }
    }

    public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress",
        typeof(double), typeof(LauncherUpdatePopup), new PropertyMetadata(OnLoadProgressChangedCallBack));

    private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        LauncherUpdatePopup progressBar = (LauncherUpdatePopup)sender;
        if (progressBar != null)
        {
            DoubleAnimation da = new DoubleAnimation()
            {
                To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d)
            };
            progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
        }
    }
}