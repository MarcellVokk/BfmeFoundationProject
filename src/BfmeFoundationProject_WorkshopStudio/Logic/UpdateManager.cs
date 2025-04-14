using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.Shared.API;
using BfmeFoundationProject.Shared.Data.Static;
using BfmeFoundationProject.Shared.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace BfmeFoundationProject.WorkshopStudio.Logic
{
    public static class UpdateManager
    {
        public static async Task<bool> CheckForUpdates()
        {
            string applicationPath = Process.GetCurrentProcess().MainModule!.FileName ?? "";

            string curentVersionHash = await Task.Run(() => FileUtils.GetFileMd5Hash(applicationPath));
            string latestVersionHash = await ApplicationsApi.GetAppVersionHash("workshop-studio", "main");

            return curentVersionHash != latestVersionHash;
        }

        public static async Task Update(string branch, Action<double> progressChanged)
        {
            string applicationPath = Process.GetCurrentProcess().MainModule!.FileName ?? "";

            File.Move(applicationPath, Path.Combine(Path.GetDirectoryName(applicationPath), $"{Path.GetFileNameWithoutExtension(applicationPath)}_old.exe"), true);

            await HttpMarshal.GetFile(
            url: $"{DeploymentConfig.ArenaFilesHost}/application-builds/workshop-studio-{branch}",
            localPath: applicationPath,
            headers: [],
            OnProgressUpdate: (downloadProgress) => progressChanged?.Invoke(downloadProgress));
        }
    }
}
