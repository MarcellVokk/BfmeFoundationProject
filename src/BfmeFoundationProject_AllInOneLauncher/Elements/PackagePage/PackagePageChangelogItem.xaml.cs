using System;
using System.Windows.Controls;

namespace BfmeFoundationProject.AllInOneLauncher.Elements
{
    /// <summary>
    /// Interaction logic for PackagePageChangelogItem.xaml
    /// </summary>
    public partial class PackagePageChangelogItem : UserControl
    {
        public PackagePageChangelogItem()
        {
            InitializeComponent();
        }

        public string Version
        {
            get => version.Text;
            set => version.Text = value;
        }

        private long _creationTime = 0;
        public long CreationTime
        {
            get => _creationTime;
            set
            {
                _creationTime = value;

                TimeSpan time = DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(value).DateTime;
                double seconds = time.TotalSeconds;

                if (seconds < 60)
                    date.Text = time.Seconds == 1 ? "one second ago" : time.Seconds + " seconds ago";
                else if (seconds < 3600)
                    date.Text = time.Minutes + " minutes ago";
                else if (seconds < 7200)
                    date.Text = "an hour ago";
                else if (seconds < 86400)
                    date.Text = time.Hours + " hours ago";
                else if (seconds < 172800)
                    date.Text = "yesterday";
                else if (seconds < 2592000)
                    date.Text = time.Days + " days ago";
                else if (seconds < 31104000)
                {
                    int months = Convert.ToInt32(Math.Floor((double)time.Days / 30));
                    date.Text = months <= 1 ? "one month ago" : months + " months ago";
                }
                else
                {
                    int years = Convert.ToInt32(Math.Floor((double)time.Days / 365));
                    date.Text = years <= 1 ? "one year ago" : years + " years ago";
                }
            }
        }

        public string Text
        {
            get => content.Text;
            set => content.Text = value;
        }
    }
}
