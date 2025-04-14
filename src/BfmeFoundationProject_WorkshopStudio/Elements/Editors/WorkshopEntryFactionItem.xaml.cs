using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.WorkshopStudio.Logic;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryFactionItem.xaml
    /// </summary>
    public partial class WorkshopEntryFactionItem : UserControl
    {
        public WorkshopEntryFactionItem()
        {
            InitializeComponent();
        }

        private BfmeFaction _faction;
        public BfmeFaction Faction
        {
            get => new BfmeFaction(name.FieldValue, id.FieldValue, _faction.BigIcon, _faction.SmallIcon);
            set
            {
                _faction = value;

                name.FieldValue = value.Name;
                id.FieldValue = value.Id;

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(value.BigIcon.Replace("STANDARD:", "pack://application:,,,/BfmeFoundationProject_WorkshopStudio;component/Resources/Images/ArmyIcons/"));
                    source.EndInit();

                    bigIcon.Source = source;
                }
                catch { }

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(value.SmallIcon.Replace("STANDARD:", $"{BfmeWorkshopManager.WorkshopFilesHost}/army-icons/"));
                    source.EndInit();

                    smallIcon.Source = source;
                }
                catch { }
            }
        }

        private void OnBigIconEnter(object sender, MouseEventArgs e) => bigIcon_editHover.Visibility = Visibility.Visible;
        private void OnBigIconLeave(object sender, MouseEventArgs e) => bigIcon_editHover.Visibility = Visibility.Hidden;

        private void OnSmallIconEnter(object sender, MouseEventArgs e) => smallIcon_editHover.Visibility = Visibility.Visible;
        private void OnSmallIconLeave(object sender, MouseEventArgs e) => smallIcon_editHover.Visibility = Visibility.Hidden;

        private async void OnBigIconClicked(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (.png)|*.png";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                string iconName = $"{Guid.NewGuid().ToString()}.png";
                await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.AuthInfo().AccountUuid, AuthManager.CurentUser.AuthInfo().AccountPassword), iconName, ofd.FileName);
                _faction.BigIcon = $"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.AuthInfo().AccountUuid}/{iconName}";

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(_faction.BigIcon.Replace("STANDARD:", "pack://application:,,,/BfmeFoundationProject_WorkshopStudio;component/Resources/Images/ArmyIcons/"));
                    source.EndInit();

                    bigIcon.Source = source;
                }
                catch { }
            }
        }

        private async void OnSmallIconClicked(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (.png)|*.png";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                string iconName = $"{Guid.NewGuid().ToString()}.png";
                await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.AuthInfo().AccountUuid, AuthManager.CurentUser.AuthInfo().AccountPassword), iconName, ofd.FileName);
                _faction.SmallIcon = $"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.AuthInfo().AccountUuid}/{iconName}";

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(_faction.SmallIcon.Replace("STANDARD:", $"{BfmeWorkshopManager.WorkshopFilesHost}/army-icons/"));
                    source.EndInit();

                    smallIcon.Source = source;
                }
                catch { }
            }
        }
    }
}
