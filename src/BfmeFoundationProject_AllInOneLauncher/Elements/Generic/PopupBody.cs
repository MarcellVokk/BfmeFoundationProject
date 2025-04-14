using System;
using System.Windows.Controls;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Generic;

public abstract class PopupBody : UserControl
{
    public virtual ColorStyle ColorStyle => ColorStyle.Regular;
    public Action<string[]>? OnSubmited;
    public Action? ClosePopup;

    public void Submit(params string[] data)
    {
        OnSubmited?.Invoke(data);
        ClosePopup?.Invoke();
    }

    public void Dismiss() => ClosePopup?.Invoke();
}