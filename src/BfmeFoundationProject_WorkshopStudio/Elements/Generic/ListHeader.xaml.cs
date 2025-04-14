using System;
using System.Windows;
using System.Windows.Controls;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for ListHeader.xaml
    /// </summary>
    public partial class ListHeader : UserControl
    {
        public ListHeader()
        {
            InitializeComponent();
        }

        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(ListHeader), new PropertyMetadata(null));

        public event EventHandler? OnAdd;
        public event EventHandler? OnAddFolder;

        public bool AddFolderEnabled
        {
            get => addFolder.Visibility == Visibility.Visible;
            set => addFolder.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            OnAdd?.Invoke(this, EventArgs.Empty);
        }

        private void AddFolder(object sender, RoutedEventArgs e)
        {
            OnAddFolder?.Invoke(this, EventArgs.Empty);
        }
    }
}
