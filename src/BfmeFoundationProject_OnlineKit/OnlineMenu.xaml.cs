using BfmeFoundationProject.OnlineKit.Data;
using BfmeFoundationProject.OnlineKit.Elements;
using BfmeFoundationProject.OnlineKit.Elements.Popups;
using BfmeFoundationProject.OnlineKit.Helpers;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace BfmeFoundationProject.OnlineKit
{
    public partial class OnlineMenu : UserControl
    {
        public bool LoadingOrLoaded { get; private set; } = false;
        public bool Loading { get; private set; } = false;
        public string UpdateBranch { get; set; } = "main";
        public string AccessToken { get; set; } = "";
        public double CornerRadius
        {
            get => mask.RadiusX;
            set { mask.RadiusX = value; mask.RadiusY = value; }
        }

        public OnlineMenu()
        {
            InitializeComponent();
            background.Source = new BitmapImage(new Uri($"{DeploymentConfig.ArenaFilesHost}/resources/background.png"));
        }

        public double Scale => TranslatePoint(new Point(0, 1), Window.GetWindow(this)).Y - TranslatePoint(new Point(0, 0), Window.GetWindow(this)).Y;

        public async Task Load(bool repairMode = false)
        {
            if (LoadingOrLoaded || Loading)
                return;

            LoadingOrLoaded = true;
            Loading = true;

            UpdateLayout();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                SetVisualState(OnlineMenuVisualState.DesignerMode);
                return;
            }

            ArenaDataHelper.EnsureDirectories();

            if (repairMode)
                SetVisualState(OnlineMenuVisualState.RepairProgress);
            else if (!ArenaDataHelper.IsInstalled)
                SetVisualState(OnlineMenuVisualState.DownloadProgress);
            else
                SetVisualState(OnlineMenuVisualState.CheckingForUpdates);

            await Task.Run(() =>
            {
                foreach (Process p in Process.GetProcessesByName("BfmeFoundationProject_OnlineArena"))
                    try { p.Kill(); } catch { }
            });

            try
            {
                if (repairMode)
                    await Task.Run(() =>
                    {
                        if (Directory.Exists(ArenaDataHelper.GlobalInstallPath))
                            Directory.Delete(ArenaDataHelper.GlobalInstallPath, true);
                    });

                if (!ArenaDataHelper.IsInstalled)
                {
                    await UpdateHelper.DownloadLatest(UpdateBranch, (progress) =>
                    {
                        Dispatcher.Invoke(() => download_progressBar.Progress = progress);
                    }, Assert);
                }
                else if (await UpdateHelper.IsUpdateAvailable(UpdateBranch))
                {
                    Assert();
                    SetVisualState(OnlineMenuVisualState.UpdateProgress);
                    await UpdateHelper.DownloadLatest(UpdateBranch, (progress) =>
                    {
                        Dispatcher.Invoke(() => download_progressBar.Progress = progress);
                    }, Assert);
                }
            }
            catch (Exception ex)
            {
                if (ex is not TaskCanceledException && ex is not OperationCanceledException)
                    SetVisualState(OnlineMenuVisualState.ServerDown);

                Loading = false;
                return;
            }

            SetVisualState(OnlineMenuVisualState.Loading);

            try
            {
                await ArenaProcessHelper.Load(
                owner: this,
                OnProcessExited: async () =>
                {
                    await Unload();

                    if (File.Exists(Path.Combine(ArenaDataHelper.GlobalDataPath, "exit_feedback")))
                    {
                        string exit_feedback = File.ReadAllText(Path.Combine(ArenaDataHelper.GlobalDataPath, "exit_feedback"));
                        File.Delete(Path.Combine(ArenaDataHelper.GlobalDataPath, "exit_feedback"));

                        if (exit_feedback == "update")
                            await Load();
                    }
                },
                Assert: Assert);

                SetVisualState(OnlineMenuVisualState.Loaded);
            }
            catch (Exception ex)
            {
                if (ex is not TaskCanceledException && ex is not OperationCanceledException)
                {
                    PopupVisualizer.ShowPopup(new MessagePopup("UNEXPECTED ERROR", $"An unexpected error occured while trying to load Arena:\n{ex.ToString()}"));
                    SetVisualState(OnlineMenuVisualState.Unloaded);
                }

                Loading = false;
                return;
            }

            Loading = false;
        }

        public async Task Unload()
        {
            if (!LoadingOrLoaded)
                return;

            LoadingOrLoaded = false;

            await ArenaProcessHelper.Unload();

            await Task.Run(() =>
            {
                while (Loading)
                    Thread.Sleep(100);
            });

            SetVisualState(OnlineMenuVisualState.Unloaded);
        }

        private void SetVisualState(OnlineMenuVisualState state)
        {
            if (state == OnlineMenuVisualState.DesignerMode || state == OnlineMenuVisualState.Unloaded)
            {
                textblock_status.Text = state == OnlineMenuVisualState.DesignerMode ? "DESIGN MODE" : "NOT LOADED";

                windowGrid.Visibility = Visibility.Visible;
                grid_status.Visibility = Visibility.Visible;
                status.Visibility = Visibility.Visible;
                download.Visibility = Visibility.Collapsed;
                loading.Visibility = Visibility.Collapsed;
                status_loading.IsLoading = false;

                repair_button.Visibility = state == OnlineMenuVisualState.Unloaded ? Visibility.Visible : Visibility.Collapsed;
                reload_button.Visibility = state == OnlineMenuVisualState.Unloaded ? Visibility.Visible : Visibility.Collapsed;
                reload_button.Content = "LOAD";
            }
            else if (state == OnlineMenuVisualState.Loading || state == OnlineMenuVisualState.CheckingForUpdates)
            {
                loading_mode.Text = state == OnlineMenuVisualState.Loading ? "LOADING" : "CHECKING FOR UPDATES";

                windowGrid.Visibility = Visibility.Visible;
                grid_status.Visibility = Visibility.Visible;
                status.Visibility = Visibility.Collapsed;
                download.Visibility = Visibility.Collapsed;
                loading.Visibility = Visibility.Visible;
                status_loading.IsLoading = true;

                repair_button.Visibility = Visibility.Hidden;
                reload_button.Visibility = Visibility.Hidden;
            }
            else if (state == OnlineMenuVisualState.DownloadProgress || state == OnlineMenuVisualState.UpdateProgress || state == OnlineMenuVisualState.RepairProgress)
            {
                download_progressBar.Progress = 0;

                if (state == OnlineMenuVisualState.DownloadProgress)
                    download_mode.Text = "DOWNLOADING";
                else if (state == OnlineMenuVisualState.UpdateProgress)
                    download_mode.Text = "UPDATING";
                else if (state == OnlineMenuVisualState.RepairProgress)
                    download_mode.Text = "REPAIRING";

                windowGrid.Visibility = Visibility.Visible;
                grid_status.Visibility = Visibility.Visible;
                status.Visibility = Visibility.Collapsed;
                download.Visibility = Visibility.Visible;
                loading.Visibility = Visibility.Collapsed;
                status_loading.IsLoading = false;

                repair_button.Visibility = Visibility.Hidden;
                reload_button.Visibility = Visibility.Hidden;
            }
            else if (state == OnlineMenuVisualState.ServerDown)
            {
                textblock_status.Text = "COULDN'T CHECK FOR UPDATES";

                windowGrid.Visibility = Visibility.Visible;
                grid_status.Visibility = Visibility.Visible;
                status.Visibility = Visibility.Visible;
                download.Visibility = Visibility.Collapsed;
                loading.Visibility = Visibility.Collapsed;
                status_loading.IsLoading = false;

                repair_button.Visibility = Visibility.Hidden;
                reload_button.Visibility = Visibility.Visible;
                reload_button.Content = "RETRY";
            }
            else if (state == OnlineMenuVisualState.Loaded)
            {
                windowGrid.Visibility = Visibility.Hidden;
                grid_status.Visibility = Visibility.Collapsed;
                status.Visibility = Visibility.Collapsed;
                download.Visibility = Visibility.Collapsed;
                loading.Visibility = Visibility.Collapsed;
                status_loading.IsLoading = false;

                repair_button.Visibility = Visibility.Hidden;
                reload_button.Visibility = Visibility.Hidden;
            }
        }

        public void Assert()
        {
            if (!LoadingOrLoaded)
                throw new TaskCanceledException();
        }

        private async void OnReloadClicked(object sender, RoutedEventArgs e)
        {
            await Unload();
            await Load();
        }

        private async void OnRepairClicked(object sender, RoutedEventArgs e)
        {
            await Unload();
            await Load(repairMode: true);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                SetVisualState(OnlineMenuVisualState.DesignerMode);
            else if (!LoadingOrLoaded)
                SetVisualState(OnlineMenuVisualState.Unloaded);
        }
    }
}
