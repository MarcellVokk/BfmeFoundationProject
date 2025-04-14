using BfmeFoundationProject.BfmeKit.Data;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BfmeFoundationProject.WorkshopStudio.Elements
{
    /// <summary>
    /// Interaction logic for WorkshopEntryMapSpot.xaml
    /// </summary>
    public partial class WorkshopEntryMapSpot : UserControl
    {
        public WorkshopEntryMapSpot()
        {
            InitializeComponent();
        }

        private BfmeSpot _spot;
        public BfmeSpot Spot
        {
            get => _spot;
            set
            {
                _spot = value;

                index.Text = value.Index.ToString();

                if (value.Team == 0)
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(40, 140, 215));
                else
                    border.BorderBrush = new SolidColorBrush(Color.FromRgb(215, 70, 40));
            }
        }

        private void OnClicked(object sender, MouseButtonEventArgs e)
        {
            _spot.Team = _spot.Team == 0 ? 1 : 0;
            Spot = _spot;
        }
    }
}
