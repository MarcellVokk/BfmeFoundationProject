using System.Linq;
using System.Windows;
using BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

namespace BfmeFoundationProject.AllInOneLauncher.Popups;

public partial class ConfirmPopup : PopupBody
{
    private bool CancelSubmits = false;

    public ConfirmPopup(string title, string message, bool cancelSubmits = false)
    {
        InitializeComponent();
        this.title.Text = string.Join("", title.Split("{").Select(x => !x.Contains("}") ? x : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));
        this.message.Text = string.Join("", message.Split("{").Select(x => !x.Contains("}") ? x : ((Application.Current.FindResource(x.Split("}")[0]).ToString() ?? "") + x.Split("}")[1])));

        CancelSubmits = cancelSubmits;
    }

    private void OnConfirmClicked(object sender, RoutedEventArgs e)
    {
        Submit(true.ToString());
    }

    private void OnCancelClicked(object sender, RoutedEventArgs e)
    {
        if (CancelSubmits)
            Submit(false.ToString());
        else
            Dismiss();
    }
}