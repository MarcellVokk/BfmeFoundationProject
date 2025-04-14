using BfmeFoundationProject.AllInOneLauncher.Elements.Menues;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for CustomHintPopup.xaml
    /// </summary>
    public partial class ContextMenuShell : UserControl
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private readonly Action<ContextMenuShell, double, double>? CalcPos;
        private readonly Action? OnDestroy;

        public ContextMenuShell(FrameworkElement owner, MenuSide side, CornerRadius corners, ColorStyle colorStyle, bool fullWidth, double minWidth, double lifespan, bool closeWhenMouseLeaves, double padding, bool tint, Action? onDestroy, Action<ContextMenuShell, double, double> calcPos)
        {
            InitializeComponent();

            Owner = owner;
            Side = side;
            Corners = corners;
            ColorStyle = colorStyle;
            Width = fullWidth ? owner.ActualWidth : double.NaN;
            MinWidth = minWidth;
            Lifespan = lifespan;
            CloseWhenMouseLeaves = closeWhenMouseLeaves;
            frame_mainContent.Margin = new Thickness(padding);
            stack_mainContent.Margin = new Thickness(padding);
            this.tint.Visibility = tint ? Visibility.Visible : Visibility.Collapsed;
            OnDestroy = onDestroy;
            CalcPos = calcPos;
            Loaded += (o, e) => UpdatePos();

            if (lifespan > 0)
                BeginLife(lifespan);
        }

        public void UpdatePos() { try { CalcPos?.Invoke(this, mainGrid.ActualWidth, mainGrid.ActualHeight); } catch { } }

        public FrameworkElement Owner { get; private set; }
        public double Lifespan { get; private set; }
        public bool CloseWhenMouseLeaves {  get; private set; }

        private MenuSide _side = MenuSide.Bottom;
        public MenuSide Side
        {
            get
            {
                return _side;
            }

            set
            {
                _side = value;

                if (value == MenuSide.Right)
                {
                    mainGrid.RenderTransformOrigin = new Point(0, 0.5);
                }
                else if (value == MenuSide.Top)
                {
                    mainGrid.RenderTransformOrigin = new Point(0.5, 1);
                }
                else if (value == MenuSide.Left)
                {
                    mainGrid.RenderTransformOrigin = new Point(1, 0.5);
                }
                else if (value == MenuSide.Bottom)
                {
                    mainGrid.RenderTransformOrigin = new Point(0.5, 0);
                }

                if (value == MenuSide.BottomRight)
                {
                    mainGrid.RenderTransformOrigin = new Point(0, 0);
                }
                else if (value == MenuSide.TopLeft)
                {
                    mainGrid.RenderTransformOrigin = new Point(1, 0.5);
                }
                else if (value == MenuSide.TopRight)
                {
                    mainGrid.RenderTransformOrigin = new Point(0, 0.5);
                }

                OnPropertyChanged();
            }
        }

        private string _contentText = "";
        public string ContentText
        {
            get
            {
                return _contentText;
            }

            set
            {
                _contentText = value;
                label_text.Text = value;
                OnPropertyChanged();
            }
        }

        private Page? _contentPage = null;
        public Page? ContentPage
        {
            get
            {
                return _contentPage;
            }

            set
            {
                _contentPage = value;
                frame_mainContent.Content = value;
                OnPropertyChanged();
            }
        }

        private List<ContextMenuItem>? _contentMenu = null;
        public List<ContextMenuItem>? ContentMenu
        {
            get
            {
                return _contentMenu;
            }

            set
            {
                _contentMenu = value;

                stack_mainContent.Children.Clear();
                if (value != null)
                    foreach (var item in value)
                        stack_mainContent.Children.Add(item.GenerateElement(this));

                OnPropertyChanged();
            }
        }

        private MenuContent _contentType = MenuContent.Text;
        public MenuContent ContentType
        {
            get
            {
                return _contentType;
            }

            set
            {
                _contentType = value;

                if (value == MenuContent.Text)
                {
                    IsHitTestVisible = false;
                    label_text.Visibility = Visibility.Visible;
                    frame_mainContent.Visibility = Visibility.Collapsed;
                    stack_mainContent_scrollview.Visibility = Visibility.Collapsed;
                }
                else if (value == MenuContent.Page)
                {
                    IsHitTestVisible = true;
                    label_text.Visibility = Visibility.Collapsed;
                    frame_mainContent.Visibility = Visibility.Visible;
                    stack_mainContent_scrollview.Visibility = Visibility.Collapsed;
                }
                else if (value == MenuContent.Menu)
                {
                    IsHitTestVisible = true;
                    label_text.Visibility = Visibility.Collapsed;
                    frame_mainContent.Visibility = Visibility.Collapsed;
                    stack_mainContent_scrollview.Visibility = Visibility.Visible;
                    stack_mainContent_scrollview.MinWidth = 150;
                    stack_mainContent_scrollview.MaxHeight = 400;
                }

                OnPropertyChanged();
            }
        }

        private bool _isMenuVisible = false;
        public bool IsMenuVisible
        {
            get
            {
                return _isMenuVisible;
            }

            set
            {
                if (_isMenuVisible == value)
                    return;

                _isMenuVisible = value;
                if (value == true)
                {
                    DoubleAnimation da = new DoubleAnimation { From = 0.8f, To = 1f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                    mainGrid.RenderTransform = new ScaleTransform();
                    mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                    mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, da);

                    DoubleAnimation dao = new DoubleAnimation { From = 0f, To = 1f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                    mainGrid.BeginAnimation(OpacityProperty, dao);
                }
                else
                {
                    DoubleAnimation da = new DoubleAnimation { From = 1f, To = 0.8f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                    mainGrid.RenderTransform = new ScaleTransform();
                    mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                    mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, da);

                    DoubleAnimation dao = new DoubleAnimation { From = 1f, To = 0f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                    mainGrid.BeginAnimation(OpacityProperty, dao);
                }

                OnPropertyChanged();
            }
        }

        public CornerRadius Corners
        {
            get => border_background.CornerRadius;
            set => border_background.CornerRadius = value;
        }


        private ColorStyle colorStyle = ColorStyle.Acrylic;
        public ColorStyle ColorStyle
        {
            get => colorStyle;
            set
            {
                colorStyle = value;

                acrylicStyle.Visibility = Visibility.Collapsed;
                navyStyle.Visibility = Visibility.Collapsed;
                regularStyle.Visibility = Visibility.Collapsed;

                if (value == ColorStyle.Acrylic)
                    acrylicStyle.Visibility = Visibility.Visible;
                else if (value == ColorStyle.Navy)
                    navyStyle.Visibility = Visibility.Visible;
                else if (value == ColorStyle.Regular)
                    regularStyle.Visibility = Visibility.Visible;
            }
        }

        public void HideSubmenues()
        {
            if (_contentMenu == null) return;

            for (int i = 0; i < _contentMenu.Count; i++)
                if (_contentMenu[i] is ContextMenuSubmenuItem)
                    MenuVisualizer.HideMenuOn(stack_mainContent.Children.OfType<FrameworkElement>().ElementAt(i));
        }

        private void BeginLife(double lifespan)
        {
            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(lifespan));

                Dispatcher.Invoke(() => StartDestroy());
            });
        }

        private bool IsDestroying = false;
        public void StartDestroy()
        {
            if (!IsDestroying)
            {
                IsDestroying = true;
                MenuVisualizer.ActiveMenues.Remove(this);
                OnDestroy?.Invoke();

                DoubleAnimation da = new DoubleAnimation { From = 1f, To = 0.8f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                da.Completed += (s, e) => MenuVisualizer.DestroyMenu(this);

                mainGrid.RenderTransform = new ScaleTransform();
                mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                mainGrid.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, da);

                DoubleAnimation dao = new() { From = 1f, To = 0f, EasingFunction = new QuadraticEase(), Duration = TimeSpan.FromSeconds(0.075) };
                mainGrid.BeginAnimation(OpacityProperty, dao);
            }
        }
    }
}
