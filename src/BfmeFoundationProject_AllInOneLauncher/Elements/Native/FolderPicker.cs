using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace BfmeFoundationProject.AllInOneLauncher.Elements.Native;

internal static class FolderPicker
{
    public static string? ShowDialog(Window owner, string title = "", string? initialPath = null)
    {
        IntPtr ownerHandle = new System.Windows.Interop.WindowInteropHelper(owner).Handle;
        return InternalShowDialog(ownerHandle, title, initialPath);
    }

    private static string? InternalShowDialog(IntPtr ownerHandle, string title, string? initialPath)
    {
            // Constants
            const int MAX_PATH = 260;
            const uint BIF_RETURNONLYFSDIRS = 0x0001;
            const uint BIF_NEWDIALOGSTYLE = 0x0040;
            const uint BIF_EDITBOX = 0x0010;
            const uint BIF_SHAREABLE = 0x8000;
            const int BFFM_INITIALIZED = 1;
            const int BFFM_SETSELECTIONW = 0x0467; // WM_USER + 103 (Unicode)

            BrowseCallbackProc callback = (hwnd, msg, lParam, lpData) =>
            {
                if (msg == BFFM_INITIALIZED && lpData != IntPtr.Zero)
                {
                    // lpData contains pointer to initial path (Unicode)
                    SendMessage(hwnd, BFFM_SETSELECTIONW, new IntPtr(1), lpData);
                }
                return 0;
            };

            IntPtr pidl = IntPtr.Zero;
            IntPtr pszDisplayName = IntPtr.Zero;
            IntPtr initialPathPtr = IntPtr.Zero;
            try
            {
                pszDisplayName = Marshal.AllocHGlobal(MAX_PATH * 2);

                var bi = new BROWSEINFO
                {
                    hwndOwner = ownerHandle,
                    pidlRoot = IntPtr.Zero,
                    pszDisplayName = pszDisplayName,
                    lpszTitle = title,
                    ulFlags = BIF_RETURNONLYFSDIRS | BIF_NEWDIALOGSTYLE | BIF_EDITBOX | BIF_SHAREABLE,
                    lpfn = callback,
                    lParam = IntPtr.Zero,
                    iImage = 0
                };

                if (!string.IsNullOrWhiteSpace(initialPath))
                {
                    initialPathPtr = Marshal.StringToHGlobalUni(initialPath);
                    bi.lParam = initialPathPtr;
                }

                pidl = SHBrowseForFolder(ref bi);
                if (pidl == IntPtr.Zero)
                    return null;

                var sb = new StringBuilder(MAX_PATH);
                if (SHGetPathFromIDList(pidl, sb))
                {
                    return sb.ToString();
                }

                return null;
            }
            finally
            {
                if (initialPathPtr != IntPtr.Zero) Marshal.FreeHGlobal(initialPathPtr);
                if (pszDisplayName != IntPtr.Zero) Marshal.FreeHGlobal(pszDisplayName);
                if (pidl != IntPtr.Zero) CoTaskMemFree(pidl);
            }
    }

    #region Native
    private delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct BROWSEINFO
    {
        public IntPtr hwndOwner;
        public IntPtr pidlRoot;
        public IntPtr pszDisplayName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszTitle;
        public uint ulFlags;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public BrowseCallbackProc lpfn;
        public IntPtr lParam;
        public int iImage;
    }

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SHBrowseForFolder([In] ref BROWSEINFO lpbi);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    private static extern bool SHGetPathFromIDList(IntPtr pidl, [Out] StringBuilder pszPath);

    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    private static extern void CoTaskMemFree(IntPtr pv);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
    #endregion
}
