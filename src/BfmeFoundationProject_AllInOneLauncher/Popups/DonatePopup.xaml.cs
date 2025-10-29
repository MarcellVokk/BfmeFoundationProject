using System.Diagnostics;
using System.Linq;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class DonatePopup : PopupBody
{
    public DonatePopup()
    {
        InitializeComponent();
    }

    private void OnOkayClicked(object sender, RoutedEventArgs e)
    {
        Dismiss();
    }

    private void OnPatreonClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://patreon.com/bfmefoundation?utm_medium=unknown&utm_source=join_link&utm_campaign=creatorshare_creator&utm_content=copyLink",
            UseShellExecute = true
        });

        PopupVisualizer.ShowPopup(new MessagePopup("THANK YOU!", "Thank you for your support, you just made our day!"));
        Dismiss();
    }

    private void OnPaypalClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "https://paypal.me/gazdagmarcell?country.x=HU&locale.x=en_US",
            UseShellExecute = true
        });

        PopupVisualizer.ShowPopup(new MessagePopup("THANK YOU!", "Thank you for your support, you just made our day!"));
        Dismiss();
    }
}