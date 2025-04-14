using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Logic;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.WorkshopStudio.Logic;
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

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryFactionLibrary.xaml
    /// </summary>
    public partial class WorkshopEntryFactionLibrary : UserControl
    {
        public WorkshopEntryFactionLibrary()
        {
            InitializeComponent();
        }

        public Func<Task>? OnSyncRequested;

        public bool ReadOnly { get; set; } = false;

        public bool IsUpToDate = false;

        private bool IsShown = false;

        public int Game { get; set; } = 0;
        public List<BfmeFaction> InitialFactions { get; set; } = new List<BfmeFaction>();
        public List<BfmeFaction> Factions => factions.Children.OfType<WorkshopEntryFactionItem>().Select(x => x.Faction).ToList();

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsShown = (bool)e.NewValue;
            Reload();
        }

        private async void Reload()
        {
            if (!spinner.IsLoading && !IsUpToDate && IsShown)
            {
                spinner.IsLoading = true;
                loadingStatus.Visibility = Visibility.Visible;
                factionCount.Text = $"0 factions";

                factions.Children.Clear();

                loadingStatus.Text = "SYNCING";

                if (!ReadOnly && OnSyncRequested != null)
                    await OnSyncRequested.Invoke();

                loadingStatus.Text = "IMPORTING FACTIONS";

                foreach (BfmeFaction faction in (ReadOnly ? InitialFactions : await Task.Run(() => BfmeFactionImporter.ImportFactions(Game))))
                    factions.Children.Add(new WorkshopEntryFactionItem() { Faction = InitialFactions.Any(x => x.Id == faction.Id) ? new BfmeFaction(InitialFactions.First(x => x.Id == faction.Id).Name, faction.Id, InitialFactions.First(x => x.Id == faction.Id).BigIcon, InitialFactions.First(x => x.Id == faction.Id).SmallIcon) : faction, Margin = new Thickness(0, 0, 0, 10) });

                spinner.IsLoading = false;
                loadingStatus.Visibility = Visibility.Collapsed;
                factionCount.Text = $"{factions.Children.OfType<WorkshopEntryFactionItem>().Count()} factions";
                IsUpToDate = true;
            }
        }

        public void Invalidate()
        {
            if (IsUpToDate)
                InitialFactions = Factions.ToList();

            IsUpToDate = false;
            factions.Children.Clear();
            Reload();
        }
    }
}
