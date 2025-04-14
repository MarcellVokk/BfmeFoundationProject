using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.OnlineKit.Data;
using System.IO;
using System.Security.Cryptography;

namespace BfmeFoundationProject.OnlineKit.Helpers
{
    internal static class UpdateHelper
    {
        public static async Task<bool> IsUpdateAvailable(string branch)
        {
            string latestVersionHash = await HttpMarshal.GetString(url: $"{DeploymentConfig.ArenaServerHost}/api/applications/versionHash?name=online-arena&version={branch}", headers: []);
            string curentVersionHash = await GetCurentVersionHash();

            return latestVersionHash != curentVersionHash;
        }

        public static async Task DownloadLatest(string branch, Action<double> progressChanged, Action assert)
        {
            ArenaDataHelper.EnsureDirectories();

            if (File.Exists(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe")))
                File.Delete(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"));

            await HttpMarshal.GetFile(
            url: $"{DeploymentConfig.ArenaFilesHost}/application-builds/online-arena-{branch}",
            localPath: Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"),
            headers: [],
            OnProgressUpdate: (progress) =>
            {
                progressChanged?.Invoke(progress);
                assert?.Invoke();
            });
        }

        private static async Task<string> GetCurentVersionHash()
        {
            if (!File.Exists(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe")))
                return "not_installed";

            return await Task.Run(() =>
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.Open(Path.Combine(ArenaDataHelper.GlobalInstallPath, "BfmeFoundationProject_OnlineArena.exe"), FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            });
        }
    }
}
