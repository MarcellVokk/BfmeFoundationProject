using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.Shared.Utils;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryFileBrowser.xaml
    /// </summary>
    public partial class WorkshopEntryFileBrowser : UserControl
    {
        public WorkshopEntryFileBrowser()
        {
            InitializeComponent();
        }

        public event EventHandler? OnFilesChanged;

        public bool ReadOnly { get; set; } = false;

        private List<BfmeWorkshopFile> files = new List<BfmeWorkshopFile>();
        public List<BfmeWorkshopFile> Files
        {
            get => files;
            set
            {
                files = value.Select(x => new BfmeWorkshopFile() { Guid = x.Guid, Language = x.Language, Md5 = x.Md5, Size = x.Size, Name = x.Name.TrimStart('\\').TrimStart('/'), Url = x.Url }).ToList();
                OpenFolder("");
            }
        }

        public string CurentFolder = "";
        public int SelectedIndex = int.MinValue;

        private void OpenFolder(string folder)
        {
            if (folder.Length > 0 && !folder.EndsWith('\\')) folder += '\\';
            CurentFolder = folder;
            SelectedIndex = int.MinValue;
            path.Text = folder;
            UpdateVirtualizer();
        }

        private void UpdateVirtualizer()
        {
            int entriesToShow = (int)(filesStackFrame.ActualHeight / (25 + 1));
            int offset = (int)scrollBar.Value;

            List<int> files = new List<int>();
            List<string> folders = new List<string>();
            for (int i = 0; i < this.files.Count; i++)
            {
                if (this.files[i].Name.StartsWith(CurentFolder))
                {
                    string[] name = this.files[i].Name.Remove(0, CurentFolder.Length).Split('\\');
                    if (name.Length == 1)
                    {
                        files.Add(i);
                    }
                    else if (!folders.Contains(name[0]))
                    {
                        folders.Add(name[0]);
                        files.Insert(0, -folders.Count);
                    }
                }
            }

            while (Math.Min(files.Count, entriesToShow) > filesStack.Children.Count)
                filesStack.Children.Add(new WorkshopEntryFileListItem() { OnSelected = OnWorkshopEntryFileListItemSelected, OnOpened = OnWorkshopEntryFileListItemOpened, OnUpdated = OnWorkshopEntryFileListItemUpdated, OnDeleted = OnWorkshopEntryFileListItemDeleted, EvalAllowEdit = (item) => !ReadOnly });

            int v = 0;
            foreach (int i in files[(Math.Clamp(offset, 0, files.Count))..Math.Clamp(offset + entriesToShow, 0, files.Count)])
            {
                WorkshopEntryFileListItem item = ((WorkshopEntryFileListItem)filesStack.Children[v]);
                if (i >= 0)
                {
                    item.File = this.files[i];
                    item.Visibility = Visibility.Visible;
                    item.Selected = SelectedIndex == i;
                    item.IsFile = true;
                    item.FileIndex = i;
                    v++;
                }
                else
                {
                    item.Folder = $@"{folders[-i - 1]}\";
                    item.Visibility = Visibility.Visible;
                    item.Selected = SelectedIndex == i;
                    item.IsFile = false;
                    item.FileIndex = i;
                    v++;
                }
            }

            while (filesStack.Children.Count > Math.Min(files.Count, entriesToShow))
                filesStack.Children.RemoveAt(filesStack.Children.Count - 1);

            scrollBar.Maximum = Math.Max(0, files.Count - entriesToShow);
        }

        public async Task UploadFiles(Action<string, int> OnProgressUpdate)
        {
            double progress = 0;
            foreach (var file in files)
            {
                progress++;
                OnProgressUpdate?.Invoke($"Processing {file.Name} ({progress}/{files.Count})", (int)(progress / files.Count * 100d));

                if (!file.Url.StartsWith("http://") && !file.Url.StartsWith("https://"))
                    await BfmeWorkshopAdminManager.UploadFile(new BfmeWorkshopAuthInfo(AuthManager.CurentUser.Uuid, AuthManager.CurentUser.Password), file.Guid, file.Url);
            }

            for (int f = 0; f < files.Count; f++)
            {
                var file = files[f];
                if (file.Url.StartsWith("http://") || file.Url.StartsWith("https://")) continue;
                file.Url = $"{BfmeWorkshopManager.WorkshopFilesHost}/{AuthManager.CurentUser.Uuid}/{file.Guid}";
                files[f] = file;
            }
        }

        private void OnWorkshopEntryFileListItemSelected(WorkshopEntryFileListItem item)
        {
            SelectedIndex = item.FileIndex;
            foreach (var e in filesStack.Children.OfType<WorkshopEntryFileListItem>())
                e.Selected = false;
        }

        private void OnWorkshopEntryFileListItemOpened(WorkshopEntryFileListItem item)
        {
            if (!item.IsFile)
                OpenFolder(CurentFolder + item.Folder);
        }

        private void OnWorkshopEntryFileListItemUpdated(WorkshopEntryFileListItem item, string newName, string oldName)
        {
            if (item.IsFile)
            {
                files[item.FileIndex] = item.File;
            }
            else
            {
                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i].Name.StartsWith(CurentFolder + oldName))
                    {
                        BfmeWorkshopFile f = files[i];
                        f.Name = f.Name.Replace(CurentFolder + oldName, CurentFolder + newName);
                        files[i] = f;
                    }
                }
            }

            OnFilesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnWorkshopEntryFileListItemDeleted(WorkshopEntryFileListItem item)
        {
            if (ReadOnly)
                return;

            if (item.IsFile)
            {
                files.RemoveAt(item.FileIndex);
                SelectedIndex = int.MinValue;
                UpdateVirtualizer();
            }
            else
            {
                files.RemoveAll(x => x.Name.StartsWith(CurentFolder + item.Folder));
                SelectedIndex = int.MinValue;
                UpdateVirtualizer();
            }

            OnFilesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnFileDrop(object sender, DragEventArgs e)
        {
            if (ReadOnly)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach (string filesystemEntry in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (filesystemEntry.Split("\\").Last().Contains("."))
                    {
                        BfmeWorkshopFile f = new BfmeWorkshopFile();
                        f.Name = Path.Combine(CurentFolder, Path.GetFileName(filesystemEntry));
                        f.Url = filesystemEntry;
                        f.Md5 = FileUtils.GetFileMd5Hash(filesystemEntry);
                        f.Size = FileUtils.GetFileSize(filesystemEntry);
                        f.Guid = f.Md5;
                        f.Language = "ALL";
                        files.RemoveAll(x => x.Name == f.Name);
                        files.Add(f);
                    }
                    else
                    {
                        foreach (string file in Directory.GetFiles(filesystemEntry, "*.*", SearchOption.AllDirectories))
                        {
                            BfmeWorkshopFile f = new BfmeWorkshopFile();
                            f.Name = CurentFolder + Path.Combine(Path.GetFileName(filesystemEntry), file.Remove(0, filesystemEntry.Length).TrimStart('\\'));
                            f.Url = file;
                            f.Md5 = FileUtils.GetFileMd5Hash(file);
                            f.Size = FileUtils.GetFileSize(file);
                            f.Guid = f.Md5;
                            f.Language = "ALL";
                            files.RemoveAll(x => x.Name == f.Name);
                            files.Add(f);
                        }
                    }
                }

                OpenFolder(CurentFolder);

                OnFilesChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnFileDragOver(object sender, DragEventArgs e)
        {
            if (ReadOnly)
                return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void OnScroll(object sender, RoutedPropertyChangedEventArgs<double> e) => UpdateVirtualizer();
        private void OnLoad(object sender, RoutedEventArgs e) => OpenFolder("");
        private void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                scrollBar.Value = Math.Clamp(scrollBar.Value - 3, 0, scrollBar.Maximum);
            else if (e.Delta < 0)
                scrollBar.Value = Math.Clamp(scrollBar.Value + 3, 0, scrollBar.Maximum);

            e.Handled = true;
        }
        private void OnPathKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                OpenFolder(path.Text);
            }
        }
        private void OnPathBackClicked(object sender, RoutedEventArgs e)
        {
            OpenFolder(string.Join("\\", CurentFolder.TrimEnd('\\').Split('\\').SkipLast(1)));
        }
    }
}