using BfmeFoundationProject.AllInOneLauncher.Elements.Menues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for DropdownPicker.xaml
    /// </summary>
    public partial class DropdownPicker : UserControl
    {
        public DropdownPicker()
        {
            InitializeComponent();
            Properties.Settings.Default.SettingsSaving += (s, e) => title.Text = Options.Count > Selected ? (Options[Selected].StartsWith("{") && Options[Selected].EndsWith("}") ? (Application.Current.FindResource(Options[Selected].TrimStart('{').TrimEnd('}')).ToString() ?? "") : Options[Selected]) : "";
            Loaded += (s, e) => title.Text = Options.Count > Selected ? (Options[Selected].StartsWith("{") && Options[Selected].EndsWith("}") ? (Application.Current.FindResource(Options[Selected].TrimStart('{').TrimEnd('}')).ToString() ?? "") : Options[Selected]) : "";
        }

        public event EventHandler? OnOptionSelected;

        private List<string> options = [];
        public List<string> Options
        {
            get => options;
            set
            {
                options = value;
                if (value.Count > Selected)
                    title.Text = value[selected];
                else
                    Selected = 0;
                MenuVisualizer.HideMenuOn(this);
            }
        }

        private int selected = 0;
        public int Selected
        {
            get => selected;
            set
            {
                if (value == int.MinValue || Options.Count <= value)
                    return;

                if (selected != value)
                {
                    selected = value;
                    title.Text = Options[value].StartsWith("{") && Options[value].EndsWith("}") ? (Application.Current.FindResource(Options[value].TrimStart('{').TrimEnd('}')).ToString() ?? "") : Options[value];
                    OnOptionSelected?.Invoke(this, EventArgs.Empty);
                }

                MenuVisualizer.HideMenuOn(border_background);
            }
        }

        public string SelectedValue
        {
            get => Options.Count > Selected ? Options[Selected] : "";
            set => Selected = Options.Contains(value) ? Options.IndexOf(value) : Selected;
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

        private double corners = 20;
        public double Corners
        {
            get => corners;
            set
            {
                corners = value;

                border_background.CornerRadius = new CornerRadius(value);
            }
        }

        private void OnDropdownClicked(object sender, RoutedEventArgs e)
        {
            if (MenuVisualizer.HasMenuOn(border_background))
                return;

            MenuVisualizer.ShowMenu(
            menu: Options.Select(x => new ContextMenuButtonItem(x, true, round: false, height: 38, clicked: () => Selected = Options.Contains(x) ? Options.IndexOf(x) : int.MinValue) as ContextMenuItem).ToList(),
            owner: border_background,
            side: MenuSide.Bottom,
            space: 0,
            corners: new CornerRadius(0, 0, Corners, Corners),
            colorStyle: ColorStyle,
            fullWidth: true,
            onDestroy: () =>
            {
                CornerRadiusAnimation ca = new() { From = new CornerRadius(Corners, Corners, 0, 0), To = new CornerRadius(Corners), Duration = TimeSpan.FromSeconds(0.075), EasingFunction = new QuadraticEase() };
                border_background.BeginAnimation(Border.CornerRadiusProperty, ca);
            });

            CornerRadiusAnimation ca = new() { From = new CornerRadius(Corners), To = new CornerRadius(Corners, Corners, 0, 0), Duration = TimeSpan.FromSeconds(0.075), EasingFunction = new QuadraticEase() };
            border_background.BeginAnimation(Border.CornerRadiusProperty, ca);
        }
    }

    public class CornerRadiusAnimation : AnimationTimeline
    {
        public override Type TargetPropertyType => typeof(CornerRadius);

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(CornerRadius), typeof(CornerRadiusAnimation));
        public CornerRadius From
        {
            get { return (CornerRadius)GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(CornerRadius), typeof(CornerRadiusAnimation));
        public CornerRadius To
        {
            get { return (CornerRadius)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(CornerRadiusAnimation));
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            CornerRadius fromVal = From;
            CornerRadius toVal = To;

            if (animationClock.CurrentProgress == null) return fromVal;
            double progress = animationClock.CurrentProgress.Value;
            if (EasingFunction != null) progress = EasingFunction.Ease(progress);

            return new CornerRadius(
                fromVal.TopLeft + (toVal.TopLeft - fromVal.TopLeft) * progress,
                fromVal.TopRight + (toVal.TopRight - fromVal.TopRight) * progress,
                fromVal.BottomRight + (toVal.BottomRight - fromVal.BottomRight) * progress,
                fromVal.BottomLeft + (toVal.BottomLeft - fromVal.BottomLeft) * progress);
        }

        protected override Freezable CreateInstanceCore() => new CornerRadiusAnimation();
    }
}

public enum ColorStyle
{
    Acrylic,
    Navy,
    Regular
}
