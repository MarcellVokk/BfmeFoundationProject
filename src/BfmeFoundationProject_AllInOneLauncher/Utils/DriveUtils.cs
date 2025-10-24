using System;
using System.IO;

namespace BfmeFoundationProject.AllInOneLauncher.Core.Utils;

public class DriveUtils
{
    /**
     Only Local Disk drives that are ready are valid.
     No Network or removable drives.
     */
    public static bool IsValidDrive(DriveInfo drive)
    {
        return drive.DriveType == DriveType.Fixed && drive.IsReady;
    }
    
    public static List<DriveInfo> GetValidDrives()
    {
        return DriveInfo.GetDrives().Where(d => IsValidDrive(d)).ToList();
    }

    public static DriveInfo? GetDriveForPath(List<DriveInfo> somedrives, string path)
    {
        DriveInfo? drive = null;

        var driveRoot = Path.GetPathRoot(path);
        if (driveRoot != null)
        {
            drive = somedrives.FirstOrDefault(d => GetDriveRootName(d).Equals(driveRoot, StringComparison.OrdinalIgnoreCase));
        }

        return drive;
    }

    public static string GetDriveRootName(DriveInfo drive)
    {
        return drive.RootDirectory.FullName;
    }
}
