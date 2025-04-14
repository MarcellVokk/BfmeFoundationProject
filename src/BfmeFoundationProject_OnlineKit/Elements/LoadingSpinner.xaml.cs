using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Animation;

namespace BfmeFoundationProject.OnlineKit.Elements
{
    /// <summary>
    /// Interaction logic for CustomLoadingElement.xaml
    /// </summary>
    internal partial class LoadingSpinner : System.Windows.Controls.UserControl
    {
        public LoadingSpinner()
        {
            InitializeComponent();
            mainStoryboard = (Storyboard)image_loading.Resources["rotateAnim"];
            mainStoryboard.Begin();
        }

        private Storyboard? mainStoryboard = null;

        private bool _isLoading = false;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;

                if (value)
                {
                    Visibility = Visibility.Visible;
                    mainStoryboard.Resume();
                }
                else
                {
                    Visibility = Visibility.Hidden;
                    mainStoryboard.Pause();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsLoading)
            {
                Visibility = Visibility.Visible;
                mainStoryboard.Resume();
            }
            else
            {
                Visibility = Visibility.Hidden;
                mainStoryboard.Pause();
            }
        }
    }
}
