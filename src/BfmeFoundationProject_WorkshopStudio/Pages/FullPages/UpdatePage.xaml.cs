using System.Windows.Controls;

namespace BfmeFoundationProject.WorkshopStudio.Pages
{
    /// <summary>
    /// Interaction logic for UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : UserControl
    {
        public UpdatePage()
        {
            InitializeComponent();
        }

        public double UpdateProgress
        {
            get => update_progressBar.Progress;
            set => update_progressBar.Progress = value;
        }
    }
}
