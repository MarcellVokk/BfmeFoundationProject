using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Logic;
using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.WorkshopStudio.Popups;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryMapLibrary.xaml
    /// </summary>
    public partial class WorkshopEntryMapLibrary : UserControl
    {
        public WorkshopEntryMapLibrary()
        {
            InitializeComponent();
        }

        public Func<Task>? OnSyncRequested;

        public bool ReadOnly { get; set; } = false;

        public bool IsUpToDate = false;

        private bool IsShown = false;

        public int Game { get; set; } = 0;
        public List<BfmeMap> InitialMaps { get; set; } = new List<BfmeMap>();
        public List<BfmeMap> Maps => maps.Children.OfType<WorkshopEntryMapItem>().Select(x => x.Map).ToList();

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsShown = (bool)e.NewValue;
            Reload();
        }

        public async Task UploadPreviews(Action<string, int> OnProgressUpdate)
        {
            double progress = 0;
            foreach (var map in maps.Children.OfType<WorkshopEntryMapItem>())
            {
                progress++;
                OnProgressUpdate?.Invoke($"Processing screenshot {progress}/{maps.Children.OfType<WorkshopEntryMapItem>().Count()}", (int)(progress / maps.Children.OfType<WorkshopEntryMapItem>().Count() * 100d));

                if (!map.Map.Preview.StartsWith("http://") && !map.Map.Preview.StartsWith("https://"))
                {
                    string previewName = $"{Guid.NewGuid().ToString()}.png";
                    await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), previewName, map.Map.Preview);
                    map.UpdatePreviewSource($"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.Uuid}/{previewName}");
                }
            }
        }

        private async void Reload()
        {
            if (!spinner.IsLoading && !IsUpToDate && IsShown)
            {
                spinner.IsLoading = true;
                loadingStatus.Visibility = Visibility.Visible;
                mapCount.Text = $"0 maps";

                maps.Children.Clear();

                loadingStatus.Text = "SYNCING";

                if (!ReadOnly && OnSyncRequested != null)
                    await OnSyncRequested.Invoke();

                loadingStatus.Text = "IMPORTING MAPS";

                foreach (BfmeMap map in (ReadOnly ? InitialMaps : await Task.Run(() => BfmeMapImporter.ImportMaps(Game))))
                    maps.Children.Add(new WorkshopEntryMapItem() { Map = InitialMaps.Any(x => x.Id == map.Id) ? new BfmeMap(map.Id, map.Name, map.Game, InitialMaps.First(x => x.Id == map.Id).Preview, map.Width, map.Height, InitialMaps.First(x => x.Id == map.Id).Spots) : map, Margin = new Thickness(0, 0, 10, 10) });

                spinner.IsLoading = false;
                loadingStatus.Visibility = Visibility.Collapsed;
                mapCount.Text = $"{maps.Children.OfType<WorkshopEntryMapItem>().Count()} maps";
                IsUpToDate = true;
            }
        }

        public void Invalidate()
        {
            if (IsUpToDate)
                InitialMaps = Maps.ToList();

            IsUpToDate = false;
            maps.Children.Clear();
            Reload();
        }
    }
}
