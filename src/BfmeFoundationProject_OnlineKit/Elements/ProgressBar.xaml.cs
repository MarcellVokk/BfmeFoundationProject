using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BfmeFoundationProject.OnlineKit.Elements
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    internal partial class ProgressBar : System.Windows.Controls.UserControl
    {
        RectangleGeometry ProgressFillClip = new RectangleGeometry() { Rect = new Rect(0, 0, 0, 0) };

        public ProgressBar()
        {
            InitializeComponent();
            progress.Clip = ProgressFillClip;
        }

        public System.Windows.Media.Brush BackgroundFillBrush
        {
            get => background.Background;
            set => background.Background = value;
        }

        public System.Windows.Media.Brush ProgressFillBrush
        {
            get => progress.Background;
            set => progress.Background = value;
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register("Progress", typeof(double), typeof(ProgressBar), new PropertyMetadata(OnProgressChangedCallBack));
        private static void OnProgressChangedCallBack(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ProgressBar progressBar = (ProgressBar)sender;
            if (progressBar != null)
            {
                double duration = (double)e.NewValue < (double)e.OldValue ? (progressBar.AnimateBackwards ? 0.5d : 0d) : (progressBar.AnimateBackwards ? 0d : 0.5d);

                RectAnimation b = new RectAnimation { From = new Rect(0, 0, progressBar.ProgressFillClip.Rect.Width, progressBar.background.ActualHeight), To = new Rect(0, 0, progressBar.background.ActualWidth * ((double)e.NewValue / 100), progressBar.background.ActualHeight), Duration = TimeSpan.FromSeconds(duration) };
                progressBar.ProgressFillClip.BeginAnimation(RectangleGeometry.RectProperty, b, HandoffBehavior.Compose);
                progressBar.ProgressText = (double)e.NewValue == 0 ? "···" : $"{(double)e.NewValue}%";
            }
        }

        public bool ShowProgressText
        {
            get => textblock_status.Visibility == Visibility.Visible;
            set => textblock_status.Visibility = value ? Visibility.Visible : Visibility.Hidden;
        }

        public string ProgressText
        {
            get => textblock_status.Text;
            set => textblock_status.Text = value;
        }

        public bool AnimateBackwards { get; set; } = false;

        private void progress_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e) => ProgressFillClip.Rect = new Rect(0, 0, ProgressFillClip.Rect.Width, e.NewSize.Height);
    }
}
