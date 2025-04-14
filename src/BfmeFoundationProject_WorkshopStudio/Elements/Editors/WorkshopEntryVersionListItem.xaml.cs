using BfmeFoundationProject.WorkshopStudio.Popups;
using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using BfmeFoundationProject.Shared.Utils;
using System.DirectoryServices;
using BfmeFoundationProject.HttpInstruments;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryVersionListItem.xaml
    /// </summary>
    public partial class WorkshopEntryVersionListItem : UserControl
    {
        public WorkshopEntryVersionListItem()
        {
            InitializeComponent();
        }

        public BfmeWorkshopEntryPreview BaseEntry {  get; set; }

        public string Version
        {
            get => name.Text;
            set => name.Text = value;
        }

        private async void OnViewClicked(object sender, RoutedEventArgs e)
        {
            var loadingPopup = new LoadingPopup();
            PopupVisualizer.ShowPopup(loadingPopup);

            var entry = await BfmeWorkshopDownloadManager.Download($"{BaseEntry.Guid}:{Version}");

            loadingPopup.Dismiss();
            PopupVisualizer.ShowPopup(new WorkshopEntryEditorPopup(entry));
        }

        private async void OnSyncClicked(object sender, RoutedEventArgs e)
        {
            sync_progress.Progress = 0;
            button_sync.IsHitTestVisible = false;
            sync_loading.IsLoading = true;
            sync_icon.Visibility = Visibility.Collapsed;

            try
            {
                var entry = await BfmeWorkshopDownloadManager.Download($"{BaseEntry.Guid}:{Version}");
                await BfmeWorkshopSyncManager.Sync(entry, enhancements: [], OnProgressUpdate: (progress, status) => Dispatcher.Invoke(() => sync_progress.Progress = progress));
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new MessagePopup("SYNC ERROR", ex.ToString()));
            }

            sync_progress.Progress = 0;
            button_sync.IsHitTestVisible = true;
            sync_loading.IsLoading = false;
            sync_icon.Visibility = Visibility.Visible;
        }
    }
}
