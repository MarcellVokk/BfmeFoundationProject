using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BfmeFoundationProject.OnlineKit.Elements
{
    /// <summary>
    /// Interaction logic for CornerAccentFrame.xaml
    /// </summary>
    internal partial class CornerAccentFrame : UserControl
    {
        public CornerAccentFrame()
        {
            InitializeComponent();
        }

        public bool ShowSides
        {
            get => sides.Visibility == Visibility.Visible;
            set => sides.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public double CornerSize
        {
            get => topLeftTop.Width;
            set
            {
                topLeftTop.Width = value;
                topLeftLeft.Height = value;
                topRightTop.Width = value;
                topRightRight.Height = value;

                bottomLeftBottom.Width = value;
                bottomLeftLeft.Height = value;
                bottomRightBottom.Width = value;
                bottomRightRight.Height = value;

                left.Margin = new Thickness(0, value + 5, 0, value + 5);
                right.Margin = new Thickness(0, value + 5, 0, value + 5);
                bottom.Margin = new Thickness(value + 5, 0, value + 5, 0);
                top.Margin = new Thickness(value + 5, 0, value + 5, 0);
            }
        }

        private bool _animate = false;
        public bool Animate
        {
            get => _animate;
            set
            {
                _animate = value;

                if (value == true && this.Visibility == Visibility.Visible)
                    StartAnimation();
                else if(value == false)
                    StopAnimation();
            }
        }

        public double AnimationGrowSize { get; set; } = 4;

        public Brush BackgroundBrush
        {
            get => background.Fill;
            set => background.Fill = value;
        }

        public double Darkness
        {
            get => background.Opacity;
            set => background.Opacity = value;
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Animate)
            {
                if (this.Visibility == Visibility.Visible)
                    StartAnimation();
                else
                    StopAnimation();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Animate && this.Visibility == Visibility.Visible)
                StartAnimation();
        }

        private void StartAnimation()
        {
            ThicknessAnimation marginAnimation = new ThicknessAnimation() { From = new Thickness(-AnimationGrowSize), To = new Thickness(0), Duration = TimeSpan.FromSeconds(0.07), EasingFunction = new QuadraticEase() };
            this.BeginAnimation(UserControl.MarginProperty, marginAnimation);

            DoubleAnimation opacityAnimation = new DoubleAnimation() { From = 0.2, To = 1, Duration = TimeSpan.FromSeconds(0.07), EasingFunction = new QuadraticEase() };
            this.BeginAnimation(UserControl.OpacityProperty, opacityAnimation);
        }

        private void StopAnimation()
        {
            this.BeginAnimation(UserControl.MarginProperty, null);
            this.BeginAnimation(UserControl.OpacityProperty, null);
        }
    }
}
