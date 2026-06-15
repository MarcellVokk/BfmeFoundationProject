using System.IO;

namespace BfmeFoundationProject.AllInOneLauncher.Core.Utils
{
    public static class InstallLocationUtils
    {
        private static readonly HashSet<string> ProgramFilesFolders = GetProgramFilesFolders();

        public static string GetDefaultDriveRoot()
        {
            var drives = DriveInfo.GetDrives();
            var drive = drives.FirstOrDefault(IsReadyFixedDrive) ?? drives.FirstOrDefault(IsReadyDrive);
            return drive?.RootDirectory.FullName ?? "";
        }

        public static bool IsValidLocation(string location)
        {
            try
            {
                return !string.IsNullOrWhiteSpace(location)
                    && Path.IsPathFullyQualified(location)
                    && !string.IsNullOrWhiteSpace(Path.GetPathRoot(location));
            }
            catch
            {
                return false;
            }
        }

        public static string NormalizeLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return "";

            try
            {
                string fullPath = Path.GetFullPath(location.Trim());
                string? root = Path.GetPathRoot(fullPath);

                if (!string.IsNullOrWhiteSpace(root) && string.Equals(fullPath, root, StringComparison.OrdinalIgnoreCase))
                    return root;

                return fullPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            catch
            {
                return location.Trim();
            }
        }

        public static bool TryGetDrive(string location, out DriveInfo? drive)
        {
            drive = null;

            try
            {
                string? root = Path.GetPathRoot(location);
                if (string.IsNullOrWhiteSpace(root))
                    return false;

                drive = new DriveInfo(root);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsInProgramFilesFolder(string location)
        {
            string normalizedLocation = NormalizeLocation(location).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return ProgramFilesFolders.Any(programFilesFolder =>
                string.Equals(normalizedLocation, programFilesFolder, StringComparison.OrdinalIgnoreCase)
                || normalizedLocation.StartsWith(programFilesFolder + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase));
        }

        private static bool IsReadyFixedDrive(DriveInfo drive) => IsReadyDrive(drive) && drive.DriveType == DriveType.Fixed;

        private static bool IsReadyDrive(DriveInfo drive)
        {
            try
            {
                return drive.IsReady;
            }
            catch
            {
                return false;
            }
        }

        private static HashSet<string> GetProgramFilesFolders()
        {
            string[] candidates =
            [
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
            ];

            return candidates
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(NormalizeLocation)
                .Select(x => x.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}
