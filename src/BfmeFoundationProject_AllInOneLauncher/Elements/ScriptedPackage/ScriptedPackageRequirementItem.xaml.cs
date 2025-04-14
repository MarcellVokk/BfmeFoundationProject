using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BfmeFoundationProject.AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for ScriptedPackageRequirementItem.xaml
    /// </summary>
    public partial class ScriptedPackageRequirementItem : UserControl
    {
        public ScriptedPackageRequirementItem()
        {
            InitializeComponent();
        }

        private string _requirementName = "";
        public string RequirementName
        {
            get => _requirementName;
            set
            {
                _requirementName = value;

                name.Text = value.Split("<")[0].Trim(' ');
                link.Visibility = value.Contains('<') && value.Contains('>') ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public int RequirementStatus
        {
            get
            {
                if (statusComplete.Visibility == Visibility.Visible)
                    return 0;
                else if (statusPending.Visibility == Visibility.Visible)
                    return 1;
                else
                    return 2;
            }
            set
            {
                statusComplete.Visibility = Visibility.Collapsed;
                statusPending.Visibility = Visibility.Collapsed;
                statusBlocked.Visibility = Visibility.Collapsed;

                if (value == 0)
                    statusComplete.Visibility = Visibility.Visible;
                else if (value == 1)
                    statusPending.Visibility = Visibility.Visible;
                else
                    statusBlocked.Visibility = Visibility.Visible;
            }
        }

        private void OnLinkClicked(object sender, MouseButtonEventArgs e) => Process.Start(new ProcessStartInfo() { FileName = RequirementName.Split("<")[1].Trim('>').Trim(' '), UseShellExecute = true });
    }
}
