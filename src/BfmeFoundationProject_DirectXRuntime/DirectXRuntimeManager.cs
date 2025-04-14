using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Versioning;

namespace BfmeFoundationProject.DirectXRuntime
{
    [SupportedOSPlatform("windows")]
    public class DirectXRuntimeManager
    {
        private static readonly string DxRuntimeVersion = "v1";

        public static async Task EnsureRuntimes()
        {
            await Task.Run(() =>
            {
                string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "Config");
                string directxDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BFME Workshop", "DirectX");

                if (!Directory.Exists(configDirectory))
                    Directory.CreateDirectory(configDirectory);

                if (!Directory.Exists(directxDirectory))
                    Directory.CreateDirectory(directxDirectory);

                bool installDxRuntime = true;

                if (File.Exists(Path.Combine(configDirectory, "dx_version.flag")))
                {
                    string[] versions = File.ReadAllText(Path.Combine(configDirectory, "dx_version.flag")).Split('\n');

                    if (versions[0] == "" || versions[0] == DxRuntimeVersion)
                        installDxRuntime = false;
                }

                if ((!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "D3DX9_43.dll")) && !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "d3dx9_43.dll"))) || (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "D3DX9_43.dll")) && !File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "d3dx9_43.dll"))))
                    installDxRuntime = true;

                foreach (string resource in Assembly.GetExecutingAssembly().GetManifestResourceNames().Where(x => x.Contains("BfmeFoundationProject.DirectXRuntime.Resources.")))
                {
                    string resourceName = resource.Replace("BfmeFoundationProject.DirectXRuntime.Resources.", "");
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource)!)
                    {
                        if (resourceName == "dx9_redist.zip" && installDxRuntime)
                        {
                            using (FileStream fs = new FileStream(Path.Combine(directxDirectory, resourceName), FileMode.Create, FileAccess.ReadWrite, FileShare.None))
                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(fs);
                            }
                        }
                    }
                }

                if (installDxRuntime)
                {
                    try
                    {
                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "D3DX9_43.dll")))
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "D3DX9_43.dll"));

                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "d3dx9_43.dll")))
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "d3dx9_43.dll"));
                    }
                    catch { }

                    try
                    {
                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "D3DX9_43.dll")))
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "D3DX9_43.dll"));

                        if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "d3dx9_43.dll")))
                            File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "d3dx9_43.dll"));
                    }
                    catch { }

                    ZipFile.ExtractToDirectory(Path.Combine(directxDirectory, "dx9_redist.zip"), Path.Combine(directxDirectory, "dx9_redist"), true);
                    if (File.Exists(Path.Combine(directxDirectory, "dx9_redist", "DXSETUP.exe")))
                        Process.Start(new ProcessStartInfo("cmd", $"/C \"{Path.Combine(directxDirectory, "dx9_redist", "DXSETUP.exe")}\" /silent") { CreateNoWindow = true })!.WaitForExit();
                }

                File.WriteAllText(Path.Combine(configDirectory, "dx_version.flag"), $"{DxRuntimeVersion}\n0");
            });
        }
    }
}
