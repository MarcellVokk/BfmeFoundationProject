using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BfmeFoundationProject.OnlineKit
{
    internal static class Win32Helper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int Width, int Height, bool Repaint);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_SIZEBOX = 0x00040000;
        private const int WS_CHILD = 0x40000000;

        public static void MakeChild(IntPtr hChild, IntPtr hParent)
        {
            int style = GetWindowLong(hChild, GWL_STYLE);
            style &= ~(WS_CAPTION | WS_SIZEBOX);
            style |= WS_CHILD;
            SetWindowLong(hChild, GWL_STYLE, (uint)(style));

            SetParent(hChild, hParent);
        }
    }
}
