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
using System.Windows.Shapes;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryDependencyListItem.xaml
    /// </summary>
    public partial class WorkshopEntryDependencyListItem : UserControl
    {
        public WorkshopEntryDependencyListItem()
        {
            InitializeComponent();
        }

        public Action? OnRemove;

        public string FullDependencyGuid { get; set; } = "";

        private BfmeWorkshopEntryPreview dependencyEntry;
        public BfmeWorkshopEntryPreview DependencyEntry
        {
            get => dependencyEntry;
            set
            {
                dependencyEntry = value;

                try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
                title.Text = value.Name;
                version.Text = value.Version;
            }
        }

        private void OnDeleteClicked(object sender, RoutedEventArgs e)
        {
            OnRemove?.Invoke();
        }
    }
}
