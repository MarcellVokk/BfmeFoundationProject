using BfmeFoundationProject.SharedUi.Elements;
using BfmeFoundationProject.WorkshopStudio.Logic;
using BfmeFoundationProject.WorkshopStudio.Pages.SubPages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BfmeFoundationProject.WorkshopStudio.Pages
{
    /// <summary>
    /// Interaction logic for MissionControlPage.xaml
    /// </summary>
    public partial class MissionControlPage : UserControl
    {
        public MissionControlPage()
        {
            InitializeComponent();

            displayName.Text = AuthManager.CurentUser.DisplayName;
            avatar.Source = new BitmapImage(new Uri($"https://bfmeladder.com/api/accounts/avatar?id={AuthManager.CurentUser.Uuid}"));

            badgeStack.Children.Clear();
            foreach (var badge in AuthManager.CurentUser.GetBadges())
            {
                if (badge == "founder")
                    badgeStack.Children.Add(new FounderBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "developer")
                    badgeStack.Children.Add(new DeveloperBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "manager")
                    badgeStack.Children.Add(new ManagerBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "organizer")
                    badgeStack.Children.Add(new OrganizerBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "moderator")
                    badgeStack.Children.Add(new ModeratorBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "supporter")
                    badgeStack.Children.Add(new SupporterBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "twitch")
                    badgeStack.Children.Add(new TwitchStreamerBadge() { Margin = new Thickness(0, 0, 2, 0), Width = 16, Height = 16 });
                else if (badge == "live")
                    badgeStack.Children.Add(new StreamLiveBadge() { Margin = new Thickness(0, 0, 2, 0), Height = 16 });
            }
        }
    }
}
