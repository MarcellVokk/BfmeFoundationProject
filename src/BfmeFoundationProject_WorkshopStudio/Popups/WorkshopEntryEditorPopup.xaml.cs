using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.Shared.Data.Maps;
using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.WorkshopKit.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.WorkshopKit.Logic;
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;
using BfmeFoundationProject.WorkshopStudio.Elements;
using System.Windows.Controls;
using BfmeFoundationProject.Shared.Utils;
using Newtonsoft.Json.Linq;
using BfmeFoundationProject.BfmeKit.Data;

namespace BfmeFoundationProject.WorkshopStudio.Popups
{
    /// <summary>
    /// Interaction logic for WorkshopEntryEditorPopup.xaml
    /// </summary>
    public partial class WorkshopEntryEditorPopup : PopupBody
    {
        public WorkshopEntryEditorPopup(BfmeWorkshopEntry entry, string submitButton = "", string dismissButton = "CLOSE")
        {
            InitializeComponent();
            game.Options = new Dictionary<string, string>()
            {
                { "0", "BFME1" },
                { "1", "BFME2" },
                { "2", "RotWK" }
            }.ToList();
            type.Options = new Dictionary<string, string>()
            {
                { "0", "Patch" },
                { "1", "Mod" },
                { "2", "Enhancement" },
                { "3", "Map Pack" }
            }.ToList();
            mapLibrary.OnSyncRequested = Sync;
            factionLibrary.OnSyncRequested = Sync;
            tabs.IsEnabledEval = (tab) =>
            {
                if (tab == "MAPS")
                    return int.Parse(type.FieldValue) != 2;
                else if (tab == "FACTIONS")
                    return int.Parse(type.FieldValue) <= 1;

                return true;
            };

            if (submitButton == "")
            {
                name.IsReadOnly = true;
                version.IsReadOnly = true;
                author.IsReadOnly = true;
                game.IsReadOnly = true;
                type.IsReadOnly = true;
                description.IsReadOnly = true;
                changelog.IsReadOnly = true;
                artwork.IsHitTestVisible = false;
                fileBrowser.ReadOnly = true;
                screenshotLibrary.ReadOnly = true;
                mapLibrary.ReadOnly = true;
                factionLibrary.ReadOnly = true;
                dependenciesHeader.IsHitTestVisible = false;
                dependenciesHeaderGuid.IsHitTestVisible = false;
                dependenciesHeaderGuid.Opacity = 0.4;
                dependencies.IsHitTestVisible = false;
                button_import.IsHitTestVisible = false;
                button_import.Opacity = 0.4;
            }

            buttonSubmit.Visibility = submitButton == "" ? Visibility.Collapsed : Visibility.Visible;
            buttonSubmit.Content = submitButton;

            buttonCancel.Visibility = dismissButton == "" ? Visibility.Collapsed : Visibility.Visible;
            buttonCancel.Content = dismissButton;

            Entry = entry;

            guid.IsReadOnly = !AuthManager.CurentUser.SystemPermissions.EditWorkshop;
        }

        private string Owner = "";
        private long CreationTime = 0;

        public BfmeWorkshopEntry Entry
        {
            get => new BfmeWorkshopEntry() { Guid = guid.FieldValue, Name = name.FieldValue, Version = version.FieldValue, Author = author.FieldValue, Game = int.Parse(game.FieldValue), Type = int.Parse(type.FieldValue), Description = description.Text, Changelog = changelog.Text, SocialLinks = [], Language = fileBrowser.Files.All(x => x.Language == "" || x.Language == "ALL") ? "EN" : string.Join(" ", fileBrowser.Files.SelectMany(x => x.Language.Split(' ')).Where(x => x != "" && x != "ALL").Distinct()), ArtworkUrl = artwork.Entry.ArtworkUrl, ScreenshotUrls = screenshotLibrary.Screenshots, CreationTime = CreationTime, Owner = Owner, Files = fileBrowser.Files, Maps = mapLibrary.Maps, Factions = factionLibrary.Factions, Dependencies = dependencies.Children.OfType<WorkshopEntryDependencyListItem>().Select(x => x.FullDependencyGuid).ToList() };
            set
            {
                artwork.Entry = value;
                guid.FieldValue = value.Guid;
                name.FieldValue = value.Name;
                version.FieldValue = value.Version;
                author.FieldValue = value.Author;
                mapLibrary.Game = value.Game;
                factionLibrary.Game = value.Game;
                game.FieldValue = value.Game.ToString();
                type.FieldValue = value.Type.ToString();
                description.Text = value.Description;
                changelog.Text = value.Changelog;
                Owner = value.Owner;
                CreationTime = value.CreationTime;
                screenshotLibrary.Screenshots = value.ScreenshotUrls;
                fileBrowser.Files = value.Files;

                factionLibrary.Invalidate();
                factionLibrary.InitialFactions = value.Factions ?? [];

                mapLibrary.Invalidate();
                mapLibrary.InitialMaps = value.Maps ?? [];

                LoadDependencies(value.Dependencies);
            }
        }

        private async void LoadDependencies(List<string> dependencies)
        {
            var invalidDependencies = new List<string>();
            this.dependencies.Children.Clear();
            foreach (var fullDependencyGuid in dependencies)
            {
                try
                {
                    BfmeWorkshopEntryPreview dependency = await BfmeWorkshopQueryManager.Get(fullDependencyGuid);
                    if (VerifyDependency(dependency))
                    {
                        var dependencyElement = new WorkshopEntryDependencyListItem() { DependencyEntry = dependency, FullDependencyGuid = fullDependencyGuid };
                        dependencyElement.OnRemove = () => this.dependencies.Children.Remove(dependencyElement);
                        this.dependencies.Children.Add(dependencyElement);
                    }
                    else
                    {
                        invalidDependencies.Add(dependency.Name);
                    }
                }
                catch
                {
                    invalidDependencies.Add(fullDependencyGuid);
                }
            }

            if (invalidDependencies.Count > 0)
                ShowError($"Invalid dependencies not loaded: {string.Join(", ", invalidDependencies)}");
        }

        private bool VerifyDependency(BfmeWorkshopEntryPreview dependency)
        {
            if (Entry.Type == 0)
            {
                if (dependency.Type == 0 || dependency.Type == 2 || dependency.Type == 3)
                    return Entry.Game == 2 ? (dependency.Game == 1 || dependency.Game == 2) : dependency.Game == Entry.Game;
            }
            else if (Entry.Type == 1)
            {
                if (dependency.Type == 0 || dependency.Type == 2 || dependency.Type == 3)
                    return Entry.Game == 2 ? (dependency.Game == 1 || dependency.Game == 2) : dependency.Game == Entry.Game;
            }
            else if (Entry.Type == 2)
            {
                if (dependency.Type == 0 || dependency.Type == 1)
                    return Entry.Game == 1 ? (dependency.Game == 1 || dependency.Game == 2) : dependency.Game == Entry.Game;
            }
            else if (Entry.Type == 3)
            {
                if (dependency.Type == 0 || dependency.Type == 1)
                    return Entry.Game == 1 ? (dependency.Game == 1 || dependency.Game == 2) : dependency.Game == Entry.Game;
            }

            return false;
        }

        private void ShowError(string message)
        {
            error.Visibility = Visibility.Visible;
            errorText.Text = message;
        }

        private void TabChanged(object sender, EventArgs e)
        {
            generalInfoPage.Visibility = Visibility.Hidden;
            imagesPage.Visibility = Visibility.Hidden;
            filesPage.Visibility = Visibility.Hidden;
            mapsPage.Visibility = Visibility.Hidden;
            factionsPage.Visibility = Visibility.Hidden;

            if (tabs.Selected == 0)
                generalInfoPage.Visibility = Visibility.Visible;
            else if (tabs.Selected == 1)
                imagesPage.Visibility = Visibility.Visible;
            else if (tabs.Selected == 2)
                filesPage.Visibility = Visibility.Visible;
            else if (tabs.Selected == 3)
                mapsPage.Visibility = Visibility.Visible;
            else if (tabs.Selected == 4)
                factionsPage.Visibility = Visibility.Visible;
        }

        private async void OnAddDependency(object sender, EventArgs e)
        {
            try
            {
                BfmeWorkshopEntryPreview dependency = await BfmeWorkshopQueryManager.Get(dependenciesHeaderGuid.FieldValue);
                if (VerifyDependency(dependency))
                {
                    if (Entry.Dependencies.Any(x => x.Split(':').First() == dependency.Guid)) return;
                    if (dependency.Type != 0 && dependency.Type != 2 && dependency.Type != 3) return;

                    var dependencyElement = new WorkshopEntryDependencyListItem() { DependencyEntry = dependency, FullDependencyGuid = dependenciesHeaderGuid.FieldValue };
                    dependencyElement.OnRemove = () => dependencies.Children.Remove(dependencyElement);
                    dependencies.Children.Add(dependencyElement);
                    dependenciesHeaderGuid.FieldValue = "";
                }
                else
                {
                    ShowError($"\"{dependency.Name}\" cannot be added as a dependency.");
                }
            }
            catch { }
        }

        private async Task Sync()
        {
            sync_progress.Progress = 0;
            button_sync.IsHitTestVisible = false;
            sync_loading.IsLoading = true;
            sync_icon.Visibility = Visibility.Collapsed;

            try
            {
                await BfmeWorkshopSyncManager.Sync(Entry, enhancements: [], OnProgressUpdate: (progress, status) => Dispatcher.Invoke(() => sync_progress.Progress = progress));
            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());
            }

            sync_progress.Progress = 0;
            button_sync.IsHitTestVisible = true;
            sync_loading.IsLoading = false;
            sync_icon.Visibility = Visibility.Visible;
        }

        private async void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                buttonSubmit.IsHitTestVisible = false;
                buttonSubmit.Opacity = 0.4;

                buttonCancel.IsHitTestVisible = false;
                buttonCancel.Opacity = 0.4;

                root.IsHitTestVisible = false;

                buttons.Visibility = Visibility.Hidden;
                progressFrame.Visibility = Visibility.Visible;
                progressText.Text = "Preparing";

                if (name.FieldValue == "")
                    throw new Exception("The package must have a name.");

                if (description.Text == "")
                    throw new Exception("The package must have a description.");

                if (artwork.Entry.ArtworkUrl == "")
                    throw new Exception("The package must have a thumbnail.");

                if (fileBrowser.Files.Count == 0)
                    throw new Exception("The package must have at least one file.");

                if (screenshotLibrary.Screenshots.Count == 0)
                    throw new Exception("The package must have at least one screenshot.");

                if (int.Parse(type.FieldValue) != 2 && !mapLibrary.IsUpToDate)
                    throw new Exception("The package must have maps configured.");

                if (int.Parse(type.FieldValue) != 2 && mapLibrary.Maps.Any(x => x.Spots.Count % 2 == 0 && x.Spots.Count(x => x.Team == 0) != x.Spots.Count(x => x.Team == 1)))
                    throw new Exception("The maps must have spots configured in a way that both 'sides' have an equal ammount of spots.\nClick on spots to toggle which 'side' they belong to!");

                if (int.Parse(type.FieldValue) <= 1 && !factionLibrary.IsUpToDate)
                    throw new Exception("The package must have factions configured.");

                if (int.Parse(type.FieldValue) == 2 && dependencies.Children.OfType<WorkshopEntryDependencyListItem>().Count() == 0)
                    throw new Exception("The package must have at least one indicated compatible base package, because it is an enhancement package. You can indicate which base packages this enhancement is compatible with by adding them as dependencies to this package!");

                await fileBrowser.UploadFiles((status, progress) => Dispatcher.Invoke(() => { progressText.Text = status; progressBar.Progress = progress; }));
                await screenshotLibrary.UploadImages((status, progress) => Dispatcher.Invoke(() => { progressText.Text = status; progressBar.Progress = progress; }));
                await mapLibrary.UploadPreviews((status, progress) => Dispatcher.Invoke(() => { progressText.Text = status; progressBar.Progress = progress; }));

                await BfmeWorkshopAdminManager.Publish(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), Entry);
                Submit(JsonConvert.SerializeObject(Entry, Formatting.Indented));
            }
            catch (Exception ex)
            {
                ShowError($"An error occured while publishing: {ex.Message}");

                buttonSubmit.IsHitTestVisible = true;
                buttonSubmit.Opacity = 1;

                buttonCancel.IsHitTestVisible = true;
                buttonCancel.Opacity = 1;

                root.IsHitTestVisible = true;

                buttons.Visibility = Visibility.Visible;
                progressFrame.Visibility = Visibility.Hidden;
            }
        }

        private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();

        private async void OnSyncClicked(object sender, RoutedEventArgs e) => await Sync();

        private void OnImpprtClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bfme Workshop Entry Json Files|*.json";

            if (ofd.ShowDialog() == true)
                Entry = JsonConvert.DeserializeObject<BfmeWorkshopEntry>(File.ReadAllText(ofd.FileName)).WithCreationTimeSetToNow().WithThisAsBaseInfo(Entry);
        }

        private void OnExportClicked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Bfme Workshop Entry Json Files|*.json";
            sfd.FileName = $"{Entry.Guid}.json";

            if (sfd.ShowDialog() == true)
                File.WriteAllText(sfd.FileName, JsonConvert.SerializeObject(Entry, Formatting.Indented));
        }

        private void OnGuidChanged(object sender, string e)
        {
            artwork.UpdateGuid(guid.FieldValue);
        }

        private void OnGameChanged(object sender, string e)
        {
            mapLibrary.Game = int.Parse(game.FieldValue);
            mapLibrary.InitialMaps = [];
            mapLibrary.Invalidate();

            factionLibrary.Game = int.Parse(game.FieldValue);
            factionLibrary.InitialFactions = [];
            factionLibrary.Invalidate();
        }

        private void OnTypeChanged(object sender, string e)
        {
            button_sync.IsHitTestVisible = int.Parse(type.FieldValue) <= 1 ? true : false;
            button_sync.Opacity = button_sync.IsHitTestVisible ? 1 : 0.4;
            tabs.Tabs = tabs.Tabs;

            mapLibrary.Invalidate();
            mapLibrary.InitialMaps = [];

            factionLibrary.Invalidate();
            factionLibrary.InitialFactions = [];
        }

        private void OnFileBrowserFilesChanged(object sender, EventArgs e)
        {
            mapLibrary.Invalidate();
            factionLibrary.Invalidate();
        }
    }
}
