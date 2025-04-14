using BfmeFoundationProject.Shared.Data.Static;
using BfmeFoundationProject.Shared.Utils;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using BfmeFoundationProject.WorkshopStudio.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryScreenshotLibrary.xaml
    /// </summary>
    public partial class WorkshopEntryScreenshotLibrary : UserControl
    {
        public WorkshopEntryScreenshotLibrary()
        {
            InitializeComponent();
        }

        public bool ReadOnly { get; set; } = false;

        public List<string> Screenshots
        {
            get => images.Children.OfType<WorkshopEntryScreenshotItem>().Select(x => x.Url).ToList();
            set
            {
                foreach (var e in images.Children.OfType<WorkshopEntryScreenshotItem>().ToList())
                    images.Children.Remove(e);

                foreach (var image in value)
                    AddImage(image);
            }
        }

        private void AddImage(string url)
        {
            if (images.Children.OfType<WorkshopEntryScreenshotItem>().Count() <= 8)
                images.Children.Insert(images.Children.Count - 1, new WorkshopEntryScreenshotItem() { Url = url, Margin = new Thickness(0, 0, 10, 10), OnDeleted = images.Children.Remove, IsHitTestVisible = !ReadOnly });

            placeholder.Visibility = images.Children.OfType<WorkshopEntryScreenshotItem>().Count() > 8 ? Visibility.Collapsed : Visibility.Visible;
        }

        public async Task UploadImages(Action<string, int> OnProgressUpdate)
        {
            double progress = 0;
            foreach (var screenshot in images.Children.OfType<WorkshopEntryScreenshotItem>())
            {
                progress++;
                OnProgressUpdate?.Invoke($"Processing screenshot {progress}/{images.Children.OfType<WorkshopEntryScreenshotItem>().Count()}", (int)(progress / images.Children.OfType<WorkshopEntryScreenshotItem>().Count() * 100d));

                if (!screenshot.Url.StartsWith("http://") && !screenshot.Url.StartsWith("https://"))
                {
                    string screenshotName = $"{Guid.NewGuid().ToString()}.png";
                    await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), screenshotName, screenshot.Url);
                    screenshot.Url = $"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.Uuid}/{screenshotName}";
                }
            }
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (ReadOnly)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string droppedFile in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (Path.GetExtension(droppedFile) == ".png")
                    {
                        AddImage(droppedFile);
                    }
                }
            }
        }

        private void OnFileDragOver(object sender, DragEventArgs e)
        {
            if (ReadOnly)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop) && images.Children.OfType<WorkshopEntryScreenshotItem>().Count() <= 8)
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }
    }
}
