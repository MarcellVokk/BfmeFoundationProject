using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BfmeFoundationProject.AllInOneLauncher.Core;

public static class SystemDisplayManager
{
    public static List<string> GetAllSupportedResolutions()
    {
        List<string> allResolutions = [];
        DEVMODE vDevMode = new();
        int i = 0;

        while (EnumDisplaySettings(null, i, ref vDevMode))
        {
            if (vDevMode.dmDisplayFrequency == 60 && vDevMode.dmBitsPerPel == 32 && vDevMode.dmDisplayFixedOutput == 0)
            {
                string resolution = vDevMode.dmPelsWidth + " " + vDevMode.dmPelsHeight;
                allResolutions.Add(resolution);
            }

            i++;
        }

        allResolutions = allResolutions
        .Select(x => (resolution: x, width: int.Parse(x.Split(' ')[0]), height: int.Parse(x.Split(' ')[1])))
        .OrderBy(r => r.width)
        .ThenBy(r => r.height)
        .Select(r => r.resolution)
        .Skip(Math.Min(3, allResolutions.Count))
        .ToList();

        return allResolutions;
    }

    public static Size GetPrimaryScreenResolution()
    {
        using Graphics g = Graphics.FromHwnd(nint.Zero);
        return new Size((int)(Screen.PrimaryScreen!.Bounds.Width / (g.DpiX / 96)), (int)(Screen.PrimaryScreen.Bounds.Height / (g.DpiY / 96)));
    }

    [DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
    internal static extern bool EnumDisplaySettings(string? deviceName, int modeNum, ref DEVMODE devMode);

    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
        internal const int CCHDEVICENAME = 0x20;
        internal const int CCHFORMNAME = 0x20;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
    }
}