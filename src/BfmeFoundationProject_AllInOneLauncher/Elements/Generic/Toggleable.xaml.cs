using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class Toggleable : UserControl
{
    public Toggleable()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


    public event EventHandler? OnToggledChanged;

    private bool _isToggled = false;
    public bool IsToggled
    {
        get
        {
            return _isToggled;
        }

        set
        {
            if(value != _isToggled)
            {
                _isToggled = value;

                if (value)
                {
                    ThicknessAnimation ta = new() { To = new Thickness(20, 0, 0, 0), EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.2) };
                    ColorAnimation ca = new() { To = (Color)ColorConverter.ConvertFromString("#028BDB"), EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.2) };

                    border_thumb.BeginAnimation(MarginProperty, ta);
                    border_shell.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);
                }
                else
                {
                    ThicknessAnimation ta = new() { To = new Thickness(0, 0, 0, 0), EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.2) };
                    ColorAnimation ca = new() { To = (Color)ColorConverter.ConvertFromString("#26FFFFFF"), EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.2) };

                    border_thumb.BeginAnimation(MarginProperty, ta);
                    border_shell.Background.BeginAnimation(SolidColorBrush.ColorProperty, ca);
                }

                OnToggledChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged();
            }
        }
    }

    private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
    {
        IsToggled = !IsToggled;
    }
}