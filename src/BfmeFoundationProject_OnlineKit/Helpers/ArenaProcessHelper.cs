using BfmeFoundationProject.OnlineKit.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Text.Json;
using System.Windows;
using System.Windows.Interop;

namespace BfmeFoundationProject.OnlineKit.Helpers
{
    public static class ArenaProcessHelper
    {
        public static IntPtr? OverrideParentWindowHandle = null;

        private static Process? ArenaProcess;
        private static IntPtr ArenaWindow = IntPtr.Zero;
        private static NamedPipeServerStream? ArenaPipeServer = null;
        private static string ArenaPipeName = "";
        private static CancellationTokenSource? CancellationTokenSource;

        public static async Task Load(OnlineMenu owner, Action OnProcessExited, Action Assert)
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource = new CancellationTokenSource();

            ArenaProcess = null;
            ArenaPipeName = Guid.NewGuid().ToString();
            ArenaPipeServer = new NamedPipeServerStream(ArenaPipeName, PipeDirection.InOut);

            foreach (Process p in Process.GetProcessesByName("BfmeFoundationProject_OnlineArena"))
                try { p.Kill(); } catch { }
            foreach (Process p in Process.GetProcessesByName("BFMECompetetiveArena_OnlineMenu"))
                try { p.Kill(); } catch { }

            if (File.Exists(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BFMECompetetiveArena_OnlineMenu.exe")))
                try { File.Delete(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BFMECompetetiveArena_OnlineMenu.exe")); } catch { }

            FirewallHelper.AddFirewallRule("Bfme Foundation Project - Online Menu", Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"));

            owner.UpdateLayout();

            Assert?.Invoke();
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                FileName = Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"),
                Arguments = @$"--embedded ""{owner.AccessToken}"" ""{owner.UpdateBranch}"" --new --corner-radius {owner.CornerRadius} --scale {JsonSerializer.Serialize(owner.Scale)} --pipe-name {ArenaPipeName} --send-window-handle",
                UseShellExecute = true,
                WorkingDirectory = ArenaDataHelper.GlobalInstallPath
            };
            ArenaProcess = Process.Start(processStartInfo);

            if (ArenaProcess == null)
                throw new Exception("ArenaProcess was null.");

            ArenaProcess.EnableRaisingEvents = true;
            ArenaProcess.Exited += (s, e) =>
            {
                owner.Dispatcher.Invoke(() => OnProcessExited?.Invoke());
                try { CancellationTokenSource?.Cancel(); } catch { }
            };

            await ArenaPipeServer.WaitForConnectionAsync(CancellationTokenSource.Token);

            if (ArenaProcess.HasExited)
            {
                owner.Dispatcher.Invoke(() => OnProcessExited?.Invoke());
                try { CancellationTokenSource?.Cancel(); } catch { }
                throw new TaskCanceledException();
            }

            Window? hostWindow = Window.GetWindow(owner);
            if(hostWindow == null)
            {
                owner.Dispatcher.Invoke(() => OnProcessExited?.Invoke());
                try { CancellationTokenSource?.Cancel(); } catch { }
                throw new TaskCanceledException();
            }

            byte[] buffer = new byte[4];
            await ArenaPipeServer.ReadExactlyAsync(buffer, CancellationTokenSource.Token);
            ArenaWindow = BitConverter.ToInt32(buffer);

            Win32Helper.MakeChild(ArenaWindow, OverrideParentWindowHandle is not null ? OverrideParentWindowHandle.Value : new WindowInteropHelper(Window.GetWindow(owner)).Handle);

            owner.UpdateLayout();

            owner.LayoutUpdated += (s, e) =>
            {
                if(ArenaProcess != null && !ArenaProcess.HasExited && ArenaWindow != IntPtr.Zero)
                {
                    Rect absoluteRect = SizeHelper.GetAbsoluteRect(element: owner);
                    MessageArena("scale", JsonSerializer.Serialize(owner.Scale));
                    Win32Helper.MoveWindow(ArenaWindow, (int)absoluteRect.X, (int)absoluteRect.Y, (int)absoluteRect.Width, (int)absoluteRect.Height, true);
                }
            };

            hostWindow.LocationChanged += (s, e) =>
            {
                if (ArenaProcess != null && !ArenaProcess.HasExited && ArenaWindow != IntPtr.Zero)
                {
                    Rect absoluteRect = SizeHelper.GetAbsoluteRect(element: owner);
                    Win32Helper.MoveWindow(ArenaWindow, (int)absoluteRect.X, (int)absoluteRect.Y, (int)absoluteRect.Width, (int)absoluteRect.Height, true);
                }
            };
        }

        private static void MessageArena(string subject, string message)
        {
            try
            {
                if (ArenaPipeServer == null)
                    return;

                using (StreamWriter sw = new StreamWriter(ArenaPipeServer, leaveOpen: true))
                    sw.WriteLine($"{subject}~{message}");
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static async Task Unload()
        {
            if (ArenaProcess == null)
                return;

            Process p = ArenaProcess;
            IntPtr w = ArenaWindow;
            ArenaProcess = null;
            ArenaWindow = IntPtr.Zero;
            ArenaPipeServer?.Dispose();
            ArenaPipeServer = null;
            ArenaPipeName = "";

            await Task.Run(() =>
            {
                if (p != null && w != IntPtr.Zero)
                {
                    try { Win32Helper.SendMessage(w, 0x0010, IntPtr.Zero, IntPtr.Zero); } catch { }
                    p.WaitForExit();
                }

                foreach (Process p in Process.GetProcessesByName("BfmeFoundationProject_OnlineArena"))
                    try { p.Kill(); } catch { }
            });
        }
    }
}
