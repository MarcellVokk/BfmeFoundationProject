using BfmeFoundationProject.AllInOneLauncher.Data;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;
using BfmeFoundationProject.AllInOneLauncher.Popups;
using BfmeFoundationProject.BfmeKit;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BfmeFoundationProject.AllInOneLauncher.Core
{
    public static class BfmeSyncManager
    {
        public static async Task SyncPackage(string packageGuid, bool useFastFileCompare = true, List<string>? enhancements = null)
        {
            MainWindow.Instance?.OnSyncBegin();

            try
            {
                BfmeWorkshopEntry package;

                try
                {
                    package = await BfmeWorkshopDownloadManager.Download(packageGuid);
                }
                catch
                {
                    package = await BfmeWorkshopLibraryManager.Get(packageGuid);
                }

                try
                {
                    await BfmeWorkshopSyncManager.Sync(package, useFastFileCompare, enhancements);
                }
                catch (BfmeWorkshopEnhancementIncompatibleException ex)
                {
                    PopupVisualizer.ShowPopup(new MessagePopup("COMPATIBILITY ERROR", ex.Message));
                }
                catch (BfmeWorkshopScriptMissingRequirementsException)
                {
                    PopupVisualizer.ShowPopup(new ScriptedPackagePopup(package));
                }
                catch (Exception ex)
                {
                    PopupVisualizer.ShowPopup(new ErrorPopup(ex));
                }
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }

            MainWindow.Instance?.OnSyncEnd();
        }

        public static async Task EnableEnhancement(string packageGuid)
        {
            MainWindow.Instance?.OnSyncBegin();

            try
            {
                BfmeWorkshopEntry package;

                try
                {
                    package = await BfmeWorkshopDownloadManager.Download(packageGuid);
                }
                catch
                {
                    package = await BfmeWorkshopLibraryManager.Get(packageGuid);
                }

                var activeEnhancements = await BfmeWorkshopManager.GetActiveEnhancements(package.Game);
                var activePatch = await BfmeWorkshopManager.GetActivePatch(package.Game) ?? await BfmeWorkshopEntry.BaseGame(package.Game);

                await SyncPackage(activePatch.Guid, true, activeEnhancements.Values.Select(x => x.Guid).Distinct().Where(x => x.Split(':')[0] != package.Guid).Concat([packageGuid]).ToList());
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }

            MainWindow.Instance?.OnSyncEnd();
        }

        public static async Task DisableEnhancement(string packageGuid)
        {
            MainWindow.Instance?.OnSyncBegin();

            try
            {
                BfmeWorkshopEntry package;

                try
                {
                    package = await BfmeWorkshopDownloadManager.Download(packageGuid);
                }
                catch
                {
                    package = await BfmeWorkshopLibraryManager.Get(packageGuid);
                }

                var activeEnhancements = await BfmeWorkshopManager.GetActiveEnhancements(package.Game);
                var activePatch = await BfmeWorkshopManager.GetActivePatch(package.Game) ?? await BfmeWorkshopEntry.BaseGame(package.Game);

                await SyncPackage(activePatch.Guid, true, activeEnhancements.Values.Select(x => x.Guid).Distinct().Where(x => x.Split(':')[0] != package.Guid).ToList());
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }

            MainWindow.Instance?.OnSyncEnd();
        }

        public static async Task InstallGame(BfmeGame game, string selectedLanguage, string selectedLocation)
        {
            MainWindow.Instance?.OnSyncBegin();

            try
            {
                BfmeRegistryManager.CreateNewInstallRegistry(game, Path.Combine(selectedLocation, game == BfmeGame.ROTWK ? "RotWK" : $"BFME{(int)game + 1}"), selectedLanguage);
                if (game == BfmeGame.ROTWK && !BfmeRegistryManager.IsInstalled(BfmeGame.BFME2)) BfmeRegistryManager.CreateNewInstallRegistry(BfmeGame.BFME2, Path.Combine(selectedLocation, "BFME2"), selectedLanguage);

                await BfmeWorkshopSyncManager.Sync(await BfmeWorkshopEntry.BaseGame((int)game));
            }
            catch (Exception ex)
            {
                PopupVisualizer.ShowPopup(new ErrorPopup(ex));
            }

            MainWindow.Instance?.OnSyncEnd();
        }
    }
}
