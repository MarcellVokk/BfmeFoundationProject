using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public partial class PopupVisualizer : UserControl
{
    private static PopupVisualizer? Instance;
    private static readonly List<Action> PopupQueue = [];

    public static event EventHandler? OnPopupOpened;
    public static event EventHandler? OnPopupClosed;

    public static PopupBody? CurentPopup => (Instance != null && Instance.content.Child is PopupBody body && ((PopupBody)Instance.content.Child).ClosePopup != null) ? body : null;

    public PopupVisualizer()
    {
        InitializeComponent();
        Instance = this;
    }

    public static void ShowPopup(PopupBody popup, Action<string[]>? OnPopupSubmited = null, Action? OnPopupClosed = null)
    {
        if (Instance == null)
            return;

        if (Instance.root.IsHitTestVisible)
        {
            PopupQueue.Add(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.22));
                Instance.root.IsHitTestVisible = false;
                ShowPopup(popup, OnPopupSubmited);
            });
            return;
        }

        popup.OnSubmited = OnPopupSubmited;
        popup.ClosePopup = () =>
        {
            HidePopup();
            OnPopupClosed?.Invoke();
        };

        if (Instance.content.Child is PopupBody body)
        {
            body.OnSubmited = null;
            body.ClosePopup = null;
        }

        Instance.content.Child = popup;
        Instance.root.IsHitTestVisible = true;
        OnPopupOpened?.Invoke(null, EventArgs.Empty);

        Instance.popupBody.Margin = popup.Margin;
        Instance.popupBody.VerticalAlignment = popup.VerticalAlignment;
        Instance.popupBody.HorizontalAlignment = popup.HorizontalAlignment;

        popup.Margin = new Thickness(0);
        popup.VerticalAlignment = VerticalAlignment.Stretch;
        popup.HorizontalAlignment = HorizontalAlignment.Stretch;

        Instance.navyStyle.Visibility = (popup.ColorStyle == ColorStyle.Navy || popup.ColorStyle == ColorStyle.Regular) ? Visibility.Visible : Visibility.Collapsed;
        Instance.acrylicStyle.Visibility = popup.ColorStyle == ColorStyle.Acrylic ? Visibility.Visible : Visibility.Collapsed;
        ((SolidColorBrush)Instance.background.Background).Opacity = (popup.ColorStyle == ColorStyle.Navy || popup.ColorStyle == ColorStyle.Regular) ? 0.8 : 0.7;

        Instance.acrylicStyle.Children.Clear();
        if (popup.ColorStyle == ColorStyle.Acrylic) Instance.acrylicStyle.Children.Add(new Acrylic());

        Instance.InvalidateVisual();
        Instance.UpdateLayout();
        Instance.popupBody.InvalidateVisual();
        Instance.popupBody.UpdateLayout();

        double crtoy = (Instance.popupBody.ActualHeight - Math.Max(0, Instance.popupBody.ActualHeight - Math.Min(Instance.ActualHeight, Instance.popupBody.ActualHeight)) - Math.Min(Instance.ActualHeight / 2, Instance.popupBody.ActualHeight / 2)) / Instance.popupBody.ActualHeight;
        Instance.popupBody.RenderTransformOrigin = new Point(0.5, crtoy == double.NaN ? 0.5 : crtoy);
        Instance.popupBody.RenderTransform = new ScaleTransform(1, 1);

        Instance.scrollViewer.ScrollToVerticalOffset(0);

        DoubleAnimation scaleAnimation = new() { From = 0.4, To = 1, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
        Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

        DoubleAnimation opacityAnimation = new() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
        Instance.root.BeginAnimation(OpacityProperty, opacityAnimation);

        ((Panel)Instance.Parent).Children.OfType<FrameworkElement>().First(x => x.Name == "outerContent").Effect = new BlurEffect() { Radius = 8 };

        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(0.05));
            popup.Dispatcher.Invoke(() => popup.InvalidateVisual());
            await Task.Delay(TimeSpan.FromSeconds(0.05));
            popup.Dispatcher.Invoke(() => popup.InvalidateVisual());
            await Task.Delay(TimeSpan.FromSeconds(0.05));
            popup.Dispatcher.Invoke(() => popup.InvalidateVisual());
            await Task.Delay(TimeSpan.FromSeconds(0.05));
            popup.Dispatcher.Invoke(() => popup.InvalidateVisual());
        });
    }

    public static void HidePopup()
    {
        if (Instance == null)
            return;

        if (Instance.content.Child is not PopupBody)
            return;

        if (PopupQueue.Count > 0)
        {
            Action popup = PopupQueue.First();
            PopupQueue.Remove(popup);
            popup.Invoke();
        }
        else
        {
            Instance.root.IsHitTestVisible = false;
            OnPopupClosed?.Invoke(null, EventArgs.Empty);
        }

        if (Instance.content.Child is PopupBody body)
        {
            body.OnSubmited = null;
            body.ClosePopup = null;
        }

        DoubleAnimation scaleAnimation = new() { To = 0.4, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
        Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
        Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

        DoubleAnimation opacityAnimation = new() { To = 0, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
        Instance.root.BeginAnimation(OpacityProperty, opacityAnimation);

        ((Panel)Instance.Parent).Children.OfType<FrameworkElement>().First(x => x.Name == "outerContent").Effect = null;
    }

    private void OnMouseUp(object sender, MouseButtonEventArgs e)
    {
        //HidePopup();
    }
}

public class BorderSizeConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        var s = value as double? ?? 0;
        return 40;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}