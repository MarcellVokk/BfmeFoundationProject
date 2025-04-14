using System.Windows;

namespace BfmeFoundationProject.OnlineKit.Elements.Popups
{
    /// <summary>
    /// Interaction logic for MessagePopup.xaml
    /// </summary>
    internal partial class MessagePopup : PopupBody
    {
        public MessagePopup(string title, string message, bool okaySubmits = false)
        {
            InitializeComponent();

            titleText.Text = title;
            messageText.Text = message;
            OkaySubmits = okaySubmits;
        }

        private bool OkaySubmits = false;

        private void OnOkayClicked(object sender, RoutedEventArgs e)
        {
            if (OkaySubmits)
                Submit();
            else
                Dismiss();
        }
    }
}
