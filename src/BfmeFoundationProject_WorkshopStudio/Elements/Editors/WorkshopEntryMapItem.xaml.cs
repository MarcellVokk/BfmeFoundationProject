using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Logic;
using BfmeFoundationProject.Shared.Data.Static;
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
    /// Interaction logic for WorkshopEntryMapItem.xaml
    /// </summary>
    public partial class WorkshopEntryMapItem : UserControl
    {
        public WorkshopEntryMapItem()
        {
            InitializeComponent();
        }

        private BfmeMap _map;
        public BfmeMap Map
        {
            get => new BfmeMap(_map.Id, _map.Name, _map.Game, _map.Preview, _map.Width, _map.Height, spots.Children.OfType<WorkshopEntryMapSpot>().Select(x => x.Spot).ToList());
            set
            {
                _map = value;

                name.Text = value.Name;
                LoadMapImage();
                LoadMapSpots();
            }
        }

        public void UpdatePreviewSource(string newSource) => _map.Preview = newSource;

        private void LoadMapImage()
        {
            if (_map.Preview != "")
            {
                image.Source = new BitmapImage(new Uri(_map.Preview));
            }
            else
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Maps")))
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Maps"));

                Task.Run(() =>
                {
                    try
                    {
                        BfmeMapImporter.GenerateMapPreview(_map)?.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Maps", $"{_map.Id}.png"));
                        UpdatePreviewSource(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Maps", $"{_map.Id}.png"));
                        Dispatcher.Invoke(() => image.Source = new BitmapImage(new Uri(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Maps", $"{_map.Id}.png"))));
                    }
                    catch { }
                });
            }
        }

        private void LoadMapSpots()
        {
            spots.Children.Clear();
            foreach (var spot in _map.Spots)
                spots.Children.Add(new WorkshopEntryMapSpot() { Spot = spot, Margin = new Thickness(spot.X * image.Width - 20, spot.Y * image.Height - 20, 0, 0) });
        }

        private void OnLoaded(object sender, RoutedEventArgs e) => LoadMapSpots();
    }
}
