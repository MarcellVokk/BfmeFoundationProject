using BfmeFoundationProject.WorkshopStudio.Popups;
using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.Shared.Data.Applications;
using BfmeFoundationProject.Shared.Utils;
using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.SharedUi.Elements.BuiltInPopups;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryFileListItem.xaml
    /// </summary>
    public partial class WorkshopEntryFileListItem : UserControl
    {
        public WorkshopEntryFileListItem()
        {
            InitializeComponent();
        }

        public Action<WorkshopEntryFileListItem>? OnSelected;
        public Action<WorkshopEntryFileListItem>? OnOpened;
        public Action<WorkshopEntryFileListItem, string, string>? OnUpdated;
        public Action<WorkshopEntryFileListItem>? OnDeleted;
        public Func<WorkshopEntryFileListItem, bool>? EvalAllowEdit;

        public bool Selected
        {
            get => selectedBackground.Visibility == Visibility.Visible;
            set
            {
                selectedBackground.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                selectedBorder.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                if (!value)
                {
                    language.Visibility = Visibility.Visible;
                    languageEdit.Visibility = Visibility.Hidden;
                    name.Visibility = Visibility.Visible;
                    nameEdit.Visibility = Visibility.Hidden;
                }
            }
        }

        public int FileIndex { get; set; } = 0;
        public bool IsFile
        {
            get => fileIcon.Visibility == Visibility.Visible;
            set
            {
                fileIcon.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                folderIcon.Visibility = value ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public string Folder
        {
            get => name.Text;
            set
            {
                name.Text = value;
                hash.Text = "";
                language.Text = "";
            }
        }

        private string Guid = "";
        private string Url = "";
        private string Root = "";
        public BfmeWorkshopFile File
        {
            get => new BfmeWorkshopFile() { Guid = Guid, Name = Root + name.Text, Url = Url, Md5 = hash.Text, Language = language.Text };
            set
            {
                name.Text = value.Name.Replace("/", "\\").TrimStart('\\').Split('\\').Last();
                hash.Text = value.Md5;
                language.Text = value.Language;
                Guid = value.Guid;
                Url = value.Url;
                Root = string.Join("\\", value.Name.Replace("/", "\\").TrimStart('\\').Split('\\').SkipLast(1));
                if (Root.Length > 0) Root += '\\';
            }
        }

        private void OnLanguageMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Selected)
            {
                if (EvalAllowEdit != null && EvalAllowEdit.Invoke(this) == false)
                    return;

                language.Visibility = Visibility.Hidden;
                languageEdit.Text = language.Text;
                languageEdit.Visibility = Visibility.Visible;
                selectedBorder.Visibility = Visibility.Hidden;
                languageEdit.Focus();
            }
        }

        private void OnNameMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Selected)
            {
                if (EvalAllowEdit != null && EvalAllowEdit.Invoke(this) == false)
                    return;

                name.Visibility = Visibility.Hidden;
                nameEdit.MinWidth = name.ActualWidth + 40;
                nameEdit.Text = name.Text;
                nameEdit.Visibility = Visibility.Visible;
                selectedBorder.Visibility = Visibility.Hidden;
                nameEdit.Focus();
            }
        }

        private void OnNameEditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string oldName = name.Text;
                name.Text = nameEdit.Text;
                name.Visibility = Visibility.Visible;
                nameEdit.Visibility = Visibility.Hidden;
                selectedBorder.Visibility = Selected ? Visibility.Visible : Visibility.Hidden;
                e.Handled = true;
                OnUpdated?.Invoke(this, nameEdit.Text, oldName);
            }
        }

        private void OnLanguageEditKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                language.Text = languageEdit.Text;
                language.Visibility = Visibility.Visible;
                languageEdit.Visibility = Visibility.Hidden;
                selectedBorder.Visibility = Selected ? Visibility.Visible : Visibility.Hidden;
                e.Handled = true;
                OnUpdated?.Invoke(this, name.Text, name.Text);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (Selected && e.Key == Key.Delete)
            {
                e.Handled = true;
                OnDeleted?.Invoke(this);
            }
        }

        private DateTime LastClick = DateTime.MinValue;
        private void OnClicked(object sender, MouseButtonEventArgs e)
        {
            if (!Selected)
            {
                OnSelected?.Invoke(this);
                Selected = true;
                LastClick = DateTime.Now;
                e.Handled = true;
            }
            else if (languageEdit.Visibility != Visibility.Visible && nameEdit.Visibility != Visibility.Visible && DateTime.Now - LastClick < TimeSpan.FromSeconds(0.5))
            {
                OnOpened?.Invoke(this);
                e.Handled = true;
            }
            else
            {
                LastClick = DateTime.Now;
            }
        }

        private void OnEnter(object sender, MouseEventArgs e) => hoverBackground.Visibility = Visibility.Visible;
        private void OnLeave(object sender, MouseEventArgs e) => hoverBackground.Visibility = Visibility.Hidden;

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.KeyDown += OnKeyDown;
            Debug.WriteLine("loaded new");
        }
        private void OnUnload(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.KeyDown -= OnKeyDown;
            Debug.WriteLine("Unloaded");
        }
    }
}
