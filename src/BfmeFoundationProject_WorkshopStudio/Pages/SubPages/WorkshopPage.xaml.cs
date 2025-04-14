using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;
using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopStudio.Popups;
using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.WorkshopKit.Logic;
using Newtonsoft.Json;
using System;
using System.Linq;
using BfmeFoundationProject.Shared.Utils;
using System.IO;
using BfmeFoundationProject.BfmeKit.Data;

namespace BfmeFoundationProject.WorkshopStudio.Pages.SubPages
{
    /// <summary>
    /// Interaction logic for WorkshopPage.xaml
    /// </summary>
    public partial class WorkshopPage : UserControl
    {
        public static List<BfmeWorkshopEntryPreview> MyEntries = new List<BfmeWorkshopEntryPreview>();

        public static async Task Reload()
        {
            if (AuthManager.CurentUser.SystemPermissions.EditWorkshop)
                MyEntries = await BfmeWorkshopQueryManager.QueryAll(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password));
            else
                MyEntries = await BfmeWorkshopQueryManager.Query(ownerUuid: AuthManager.CurentUser.Uuid);
        }

        public WorkshopPage()
        {
            InitializeComponent();

            entries.GetEntryCount = () => MyEntries.Count;
            entries.SelectedEntryChanged = () =>
            {
                if (entries.SelectedEntryIndex >= MyEntries.Count)
                {
                    info.Visibility = Visibility.Hidden;
                    return;
                }

                workshopEntryVersionsEditor.FieldValue = MyEntries[entries.SelectedEntryIndex];
                detailsTitle.Title = $"[{MyEntries[entries.SelectedEntryIndex].GameName()}] {MyEntries[entries.SelectedEntryIndex].Name}";

                info.Visibility = Visibility.Visible;
            };
            entries.FilterEntry = (entryIndex) => MyEntries[entryIndex].Name.ToLower().Contains(search.Text.ToLower()) || MyEntries[entryIndex].Description.ToLower().Contains(search.Text.ToLower());
            entries.RenderEntry = (entry) =>
            {
                entry.Title = $"[{MyEntries[entry.Index].GameName()}] {MyEntries[entry.Index].Name}";
            };
        }

        private void OnCreateNewWorkshopEntry(object sender, RoutedEventArgs e)
        {
            var entry = BfmeWorkshopEntry.MakeNew(author: AuthManager.CurentUser.DisplayName, owner: AuthManager.CurentUser.Uuid);

            PopupVisualizer.ShowPopup(new WorkshopEntryEditorPopup(entry, submitButton: "PUBLISH", dismissButton: "CANCEL"),
            OnPopupSubmited: (submitedData) =>
            {
                var publishedEntry = JsonConvert.DeserializeObject<BfmeWorkshopEntryPreview>(submitedData[0]);

                if (MyEntries.Any(x => x.Guid == publishedEntry.Guid))
                    return;

                MyEntries.Add(publishedEntry);
                entries.UpdateVirtualizer();
                entries.SelectedEntryIndex = MyEntries.Count - 1;
            });
        }

        private void OnSearchFilterChanged(object sender, TextChangedEventArgs e) => entries.FilterUpdated();

        private void OnFullReloadRequested(object sender, EventArgs e) => entries.UpdateVirtualizer();
    }
}
