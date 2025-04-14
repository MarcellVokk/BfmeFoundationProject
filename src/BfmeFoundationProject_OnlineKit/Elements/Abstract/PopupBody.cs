using System.Windows.Controls;

namespace BfmeFoundationProject.OnlineKit.Elements
{
    internal abstract class PopupBody : UserControl
    {
        public Action<string[]>? OnSubmited;
        public Action? ClosePopup;

        internal void Submit(params string[] data)
        {
            OnSubmited?.Invoke(data);
            ClosePopup?.Invoke();
        }

        internal void Dismiss() => ClosePopup?.Invoke();
    }
}
