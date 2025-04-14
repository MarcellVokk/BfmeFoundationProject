using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.Shared.API;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Microsoft.Win32;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryArtworkEditor.xaml
    /// </summary>
    public partial class WorkshopEntryArtworkEditor : UserControl
    {
        public WorkshopEntryArtworkEditor()
        {
            InitializeComponent();
        }

        private BfmeWorkshopEntry _entry;
        public BfmeWorkshopEntry Entry
        {
            get => _entry;
            set
            {
                _entry = value;

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(_entry.ArtworkUrl);
                    source.EndInit();

                    background.Source = source;
                }
                catch { }
            }
        }

        public void UpdateGuid(string newGuid) => _entry.Guid = newGuid;

        private void OnEnter(object sender, System.Windows.Input.MouseEventArgs e) => editHover.Visibility = System.Windows.Visibility.Visible;
        private void OnLeave(object sender, System.Windows.Input.MouseEventArgs e) => editHover.Visibility = System.Windows.Visibility.Hidden;

        private async void OnClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (.png)|*.png";
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == true)
            {
                await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.AuthInfo().AccountUuid, AuthManager.CurentUser.AuthInfo().AccountPassword), $"{Entry.Guid}-{Entry.Version.Replace(".", "_")}-artwork.png", ofd.FileName);
                _entry.ArtworkUrl = $"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.AuthInfo().AccountUuid}/{Entry.Guid}-{Entry.Version.Replace(".", "_")}-artwork.png";

                try
                {
                    BitmapImage source = new BitmapImage();
                    source.BeginInit();
                    source.CacheOption = BitmapCacheOption.OnLoad;
                    source.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    source.UriSource = new Uri(_entry.ArtworkUrl);
                    source.EndInit();

                    background.Source = source;
                }
                catch { }
            }
        }
    }
}
