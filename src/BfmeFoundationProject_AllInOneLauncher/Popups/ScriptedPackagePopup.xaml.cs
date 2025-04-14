using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Elements;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class ScriptedPackagePopup : PopupBody
{
    public ScriptedPackagePopup(BfmeWorkshopEntry WorkshopEntry)
    {
        InitializeComponent();
        this.WorkshopEntry = WorkshopEntry;
        CheckRequirementsTimer.Elapsed += (s, e) => Dispatcher.Invoke(UpdateRequirements);
    }

    private System.Timers.Timer CheckRequirementsTimer = new System.Timers.Timer() { AutoReset = true, Interval = 5000 };

    private BfmeWorkshopEntry _workshopEntry;
    public BfmeWorkshopEntry WorkshopEntry
    {
        get => _workshopEntry;
        private set
        {
            _workshopEntry = value;

            title.Text = value.Name;
            try { icon.Source = new BitmapImage(new Uri(value.ArtworkUrl)); } catch { }
            author.Text = value.Author;

            UpdateRequirements();
        }
    }

    public async void UpdateRequirements()
    {
        var scriptArtifacts = await BfmeWorkshopScriptManager.RunIfScripted(WorkshopEntry);

        if (scriptArtifacts.requirements.Any(x => x.Value == false))
        {
            Dictionary<string, int> requirements = new Dictionary<string, int>();
            foreach (var requirement in scriptArtifacts.requirements)
                requirements.Add(requirement.Key, requirement.Value ? 0 : (requirements.Any(x => x.Value == 1) ? 2 : 1));

            if (string.Join("", requirements.Select(x => $"{x.Key},{x.Value}")) != string.Join("", this.requirements.Children.OfType<ScriptedPackageRequirementItem>().Select(x => $"{x.RequirementName},{x.RequirementStatus}")))
            {
                this.requirements.Children.Clear();
                foreach (var requirement in requirements)
                    this.requirements.Children.Add(new ScriptedPackageRequirementItem() { RequirementName = requirement.Key, RequirementStatus = requirement.Value, Margin = new Thickness(0, 0, 0, 20) });
            }
        }
        else
        {
            CheckRequirementsTimer.Stop();
            Dismiss();
            await BfmeSyncManager.SyncPackage(WorkshopEntry.Guid);
        }
    }

    private void OnLoad(object sender, RoutedEventArgs e)
    {
        CheckRequirementsTimer.Start();
    }

    private async void OnCancelClicked(object sender, RoutedEventArgs e)
    {
        CheckRequirementsTimer.Stop();
        Dismiss();
        await BfmeSyncManager.SyncPackage($"original-{WorkshopEntry.GameName()}");
    }
}