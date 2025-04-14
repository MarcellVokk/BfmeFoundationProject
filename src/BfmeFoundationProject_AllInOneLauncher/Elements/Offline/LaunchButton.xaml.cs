using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Offline;

public partial class LaunchButton : UserControl
{
    public LaunchButton()
    {
        InitializeComponent();

        BfmeWorkshopSyncManager.OnSyncBegin += OnSyncBegin;
        BfmeWorkshopSyncManager.OnSyncUpdate += OnSyncUpdate;
        BfmeWorkshopSyncManager.OnSyncEnd += OnSyncEnd;
    }

    private void OnSyncBegin(BfmeFoundationProject.WorkshopKit.Data.BfmeWorkshopEntry entry)
    {
        IsLoading = true;
        Dispatcher.Invoke(() => ButtonState = _buttonState);
    }

    private void OnSyncUpdate(int progress, string status)
    {
        Dispatcher.Invoke(() =>
        {
            LoadProgress = progress;
            LoadStatus = status;
        });
    }

    private void OnSyncEnd()
    {
        IsLoading = false;
        Dispatcher.Invoke(() => ButtonState = _buttonState);
    }

    public event EventHandler? OnLaunchClicked;
    public event EventHandler? OnInstallClicked;

    private LaunchButtonState _buttonState = LaunchButtonState.Launch;
    public LaunchButtonState ButtonState
    {
        get => _buttonState;
        set
        {
            _buttonState = value;

            if (IsLoading)
            {
                button.Content = "";
                button.Opacity = 0.4d;
                button.IsHitTestVisible = false;
                LoadProgress = 0;
                progressIndication.Visibility = Visibility.Visible;
            }
            else if (value == LaunchButtonState.Launch)
            {
                button.Content = Application.Current.FindResource("GenericPlay").ToString()!;
                button.Opacity = 1d;
                button.IsHitTestVisible = true;
                LoadProgress = 0;
                progressIndication.Visibility = Visibility.Collapsed;
            }
            else if (value == LaunchButtonState.Install)
            {
                button.Content = Application.Current.FindResource("GenericInstall").ToString()!;
                button.Opacity = 1d;
                button.IsHitTestVisible = true;
                LoadProgress = 0;
                progressIndication.Visibility = Visibility.Collapsed;
            }
        }
    }

    private bool IsLoading = false;

    public double LoadProgress
    {
        get => (double)GetValue(LoadProgressProperty);
        set
        {
            SetValue(LoadProgressProperty, value);
            progressText.Text = $"{value}%";
        }
    }
    public static readonly DependencyProperty LoadProgressProperty = DependencyProperty.Register("LoadProgress", typeof(double), typeof(LaunchButton), new PropertyMetadata(OnLoadProgressChangedCallBack));
    private static void OnLoadProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        LaunchButton progressBar = (LaunchButton)sender;
        if (progressBar != null)
        {
            DoubleAnimation da = new() { To = (double)e.NewValue / 100d, Duration = TimeSpan.FromSeconds((double)e.NewValue == 0d ? 0d : 0.5d) };
            progressBar.progressGradientStop1.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
            progressBar.progressGradientStop2.BeginAnimation(GradientStop.OffsetProperty, da, HandoffBehavior.Compose);
        }
    }

    public string LoadStatus
    {
        get => statusText.Text;
        set => statusText.Text = value;
    }

    private void OnClicked(object sender, RoutedEventArgs e)
    {
        if (ButtonState == LaunchButtonState.Launch)
            OnLaunchClicked?.Invoke(this, EventArgs.Empty);
        else if (ButtonState == LaunchButtonState.Install)
            OnInstallClicked?.Invoke(this, EventArgs.Empty);
    }
}

public enum LaunchButtonState
{
    Launch,
    Install
}