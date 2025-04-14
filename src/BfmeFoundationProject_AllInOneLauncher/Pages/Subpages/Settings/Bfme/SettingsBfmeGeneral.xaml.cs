using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BfmeFoundationProject.AllInOneLauncher.Core;
using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.BfmeKit.Data;
using BfmeFoundationProject.BfmeKit.Logic;

namespace BfmeFoundationProject.AllInOneLauncher.Pages.Subpages.Settings.Bfme;

public partial class SettingsBfmeGeneral : UserControl
{
    public BfmeGame Game = BfmeGame.NONE;

    public SettingsBfmeGeneral(BfmeGame game)
    {
        Game = game;
        InitializeComponent();
        InitializePageElements();
    }

    private void InitializePageElements()
    {
        ResolutionDropdown.Options = SystemDisplayManager.GetAllSupportedResolutions();

        ResolutionDropdown.SelectedValue = BfmeSettingsManager.Get(Game, "Resolution") ?? ResolutionDropdown.Options.First();
        LanguageDropdown.SelectedValue = BfmeRegistryManager.GetKeyValue(Game, BfmeRegistryKey.Language);

        title.Text = Game switch
        {
            BfmeGame.BFME1 => Application.Current.FindResource("SettingsPageBFME1SectionHeader").ToString(),
            BfmeGame.BFME2 => Application.Current.FindResource("SettingsPageBFME2SectionHeader").ToString(),
            BfmeGame.ROTWK => Application.Current.FindResource("SettingsPageRotWKSectionHeader").ToString(),
            _ => ""
        };
    }

    private void OnLanguageOptionSelected(object sender, System.EventArgs e)
    {
        BfmeRegistryManager.SetKeyValue(Game, BfmeRegistryKey.Language, LanguageDropdown.SelectedValue);
        if (Game == BfmeGame.ROTWK)
            BfmeRegistryManager.SetKeyValue(BfmeGame.BFME2, BfmeRegistryKey.Language, LanguageDropdown.SelectedValue);
        if (Game == BfmeGame.BFME2)
            BfmeRegistryManager.SetKeyValue(BfmeGame.ROTWK, BfmeRegistryKey.Language, LanguageDropdown.SelectedValue);

        Primary.Settings.NeedsResync = true;
        Properties.Settings.Default.Save();
    }

    private void OnGameResolutionOptionSelected(object sender, System.EventArgs e)
    {
        BfmeSettingsManager.Set(Game, "Resolution", ResolutionDropdown.SelectedValue);
        Properties.Settings.Default.Save();
    }
}