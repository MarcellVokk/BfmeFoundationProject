using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace BfmeFoundationProject.OnlineKit.Elements
{
    /// <summary>
    /// Interaction logic for PopupVisualizer.xaml
    /// </summary>
    internal partial class PopupVisualizer : UserControl
    {
        private static PopupVisualizer? Instance;
        private static List<Action> PopupQueue = new List<Action>();

        internal static PopupBody? CurentPopup => (Instance != null && Instance.content.Child is PopupBody) ? (PopupBody)Instance.content.Child : null;

        public PopupVisualizer()
        {
            InitializeComponent();
            Instance = this;
        }

        internal static void ShowPopup(PopupBody popup, Action<string[]>? OnPopupSubmited = null)
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
            popup.ClosePopup = () => HidePopup();

            if (Instance.content.Child is PopupBody)
            {
                ((PopupBody)Instance.content.Child).OnSubmited = null;
                ((PopupBody)Instance.content.Child).ClosePopup = null;
            }

            Instance.content.Child = popup;
            Instance.root.IsHitTestVisible = true;

            Instance.popupBody.Margin = popup.Margin;
            Instance.popupBody.VerticalAlignment = popup.VerticalAlignment;
            Instance.popupBody.HorizontalAlignment = popup.HorizontalAlignment;

            popup.Margin = new Thickness(0);
            popup.VerticalAlignment = VerticalAlignment.Stretch;
            popup.HorizontalAlignment = HorizontalAlignment.Stretch;

            Instance.popupBody.RenderTransformOrigin = new Point(0.5, 0.5);
            Instance.popupBody.RenderTransform = new ScaleTransform(1, 1);

            DoubleAnimation scaleAnimation = new DoubleAnimation() { From = 0.4, To = 1, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
            Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            DoubleAnimation opacityAnimation = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
            Instance.root.BeginAnimation(OpacityProperty, opacityAnimation);

            (Instance.Parent as Panel).Children.OfType<FrameworkElement>().First(x => x.Name == "content").Effect = new BlurEffect() { Radius = 8 };
        }

        internal static void HidePopup()
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
            }

            if (Instance.content.Child is PopupBody)
            {
                ((PopupBody)Instance.content.Child).OnSubmited = null;
                ((PopupBody)Instance.content.Child).ClosePopup = null;
            }

            DoubleAnimation scaleAnimation = new DoubleAnimation() { To = 0.4, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
            Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            Instance.popupBody.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

            DoubleAnimation opacityAnimation = new DoubleAnimation() { To = 0, Duration = TimeSpan.FromSeconds(0.2), EasingFunction = new QuadraticEase() };
            Instance.root.BeginAnimation(OpacityProperty, opacityAnimation);

            (Instance.Parent as Panel).Children.OfType<FrameworkElement>().First(x => x.Name == "content").Effect = new BlurEffect() { Radius = 0 };
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            //HidePopup();
        }
    }
}
