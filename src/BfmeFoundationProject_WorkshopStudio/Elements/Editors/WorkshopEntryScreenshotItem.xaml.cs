using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryScreenshotItem.xaml
    /// </summary>
    public partial class WorkshopEntryScreenshotItem : UserControl
    {
        public WorkshopEntryScreenshotItem()
        {
            InitializeComponent();
        }

        public Action<WorkshopEntryScreenshotItem>? OnDeleted;

        private string _url = "";
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                try { image.Source = new BitmapImage(new Uri(value)); } catch { }
            }
        }

        private void OnEnter(object sender, MouseEventArgs e)
        {
            hover.Visibility = Visibility.Visible;
            delete.Visibility = Visibility.Visible;
        }

        private void OnLeave(object sender, MouseEventArgs e)
        {
            hover.Visibility = Visibility.Collapsed;
            delete.Visibility = Visibility.Collapsed;
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            OnDeleted?.Invoke(this);
        }
    }
}
