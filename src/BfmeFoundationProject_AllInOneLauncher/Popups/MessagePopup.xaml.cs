using System.Linq;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class MessagePopup : PopupBody
{
    public MessagePopup(string title, string message)
    {
        InitializeComponent();
        this.title.Text = string.Join("",
            title.Split("{").Select(x =>
                !x.Contains("}")
                    ? x
                    : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));
        this.message.Text = string.Join("",
            message.Split("{").Select(x =>
                !x.Contains("}")
                    ? x
                    : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e) => Dismiss();
}