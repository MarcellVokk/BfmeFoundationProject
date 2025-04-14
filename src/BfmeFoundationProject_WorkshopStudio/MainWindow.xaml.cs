using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.SharedUi.Elements;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using BfmeFoundationProject.WorkshopStudio.Logic;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Diagnostics;

namespace BfmeFoundationProject.WorkshopStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
            CheckSize();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) => CheckSize();

        public void CheckSize()
        {
            var dpi = VisualTreeHelper.GetDpi(this);
            windowGrid.LayoutTransform = new ScaleTransform(1 / dpi.DpiScaleX * Math.Min(1, Math.Min((this.ActualWidth / 1500), (this.ActualHeight / 1000))), 1 / dpi.DpiScaleX * Math.Min(1, Math.Min((this.ActualWidth / 1500), (this.ActualHeight / 1000))));
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F1)
                AuthManager.Logout();
        }
    }
}