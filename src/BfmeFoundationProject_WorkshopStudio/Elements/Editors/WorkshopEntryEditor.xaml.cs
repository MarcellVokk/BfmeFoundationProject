using BfmeFoundationProject.WorkshopStudio.Popups;
using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
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
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using BfmeFoundationProject.WorkshopStudio.Pages.SubPages;
using System.Diagnostics;
using BfmeFoundationProject.WorkshopStudio.Pages;
using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.Shared.Utils;
using BfmeFoundationProject.HttpInstruments;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryEditor.xaml
    /// </summary>
    public partial class WorkshopEntryEditor : UserControl
    {
        public WorkshopEntryEditor()
        {
            InitializeComponent();
        }

        public event EventHandler? FullReloadRequested;

        BfmeWorkshopEntryPreview fieldValue;
        public BfmeWorkshopEntryPreview FieldValue
        {
            get => fieldValue;
            set
            {
                fieldValue = value;
                LoadVersions();
            }
        }

        private async void LoadVersions()
        {
            versions.Children.Clear();
            var entry = await BfmeWorkshopQueryManager.Get(FieldValue.Guid);
            versions.Children.Clear();
            foreach (var version in entry.Metadata.Versions.Reverse<string>())
                versions.Children.Add(new WorkshopEntryVersionListItem() { BaseEntry = FieldValue, Version = version });
        }

        private async void OnCreateNewUpdate(object sender, RoutedEventArgs e)
        {
            var loadingPopup = new LoadingPopup();
            PopupVisualizer.ShowPopup(loadingPopup);

            var entry = (await BfmeWorkshopDownloadManager.Download(FieldValue.Guid)).WithCreationTimeSetToNow().WithVersionBumped();
            loadingPopup.Dismiss();

            PopupVisualizer.ShowPopup(new WorkshopEntryEditorPopup(entry, submitButton: "PUBLISH", dismissButton: "CANCEL"),
            OnPopupSubmited: (submitedData) =>
            {
                LoadVersions();
            });
        }

        private void OnTransfer(object sender, RoutedEventArgs e)
        {
            PopupVisualizer.ShowPopup(new TextInputPopup("TRANSFER WORKSHOP ENTRY", "Enter the GUID of the account you wish to transfer the package to.", "BENIFICIARY GUID"),
            OnPopupSubmited: async (submitedData) =>
            {
                try
                {
                    await BfmeWorkshopAdminManager.Transfer(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), FieldValue.Guid, submitedData[0]);
                    await WorkshopPage.Reload();
                    PopupVisualizer.ShowPopup(new MessagePopup("SUCCESS", $"The transfer was completed successfuly!"));
                }
                catch(Exception ex)
                {
                    PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"Error while transfering workshop package: {ex.Message}"));
                }

                FullReloadRequested?.Invoke(this, EventArgs.Empty);
            });
        }

        private void OnUnlist(object sender, RoutedEventArgs e)
        {
            PopupVisualizer.ShowPopup(new ConfirmPopup("UNLIST WORKSHOP ENTRY", $"You are about to unlist \"{FieldValue.Name}\". Workshop packages stay in players libraries even after unlisting, but are not discoverable on the Workshop.", "UNLIST"),
            OnPopupSubmited: async (submitedData) =>
            {
                try
                {
                    await BfmeWorkshopAdminManager.Unlist(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), FieldValue.Guid);
                    await WorkshopPage.Reload();
                    PopupVisualizer.ShowPopup(new MessagePopup("SUCCESS", $"The package was unlisted successfuly!"));
                }
                catch (Exception ex)
                {
                    PopupVisualizer.ShowPopup(new MessagePopup("ERROR", $"Error while unlisting workshop package: {ex.Message}"));
                }

                FullReloadRequested?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
