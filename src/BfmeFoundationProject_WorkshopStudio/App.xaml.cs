using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace BfmeFoundationProject.WorkshopStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsReleaseBuild = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            IsReleaseBuild = !e.Args.Contains("DEBUG");

            try
            {
                string applicationPath = Process.GetCurrentProcess().MainModule!.FileName ?? "";

                if (File.Exists(Path.Combine(Path.GetDirectoryName(applicationPath), $"{Path.GetFileNameWithoutExtension(applicationPath)}_old.exe")))
                    File.Delete(Path.Combine(Path.GetDirectoryName(applicationPath), $"{Path.GetFileNameWithoutExtension(applicationPath)}_old.exe"));
            }
            catch { }
        }
    }
}
