using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace BfmeFoundationProject.WorkshopKit.Utils
{
    internal static class FileUtils
    {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        internal static async Task<string> Hash(string path)
        {
            if (!File.Exists(path))
                return "-1";

            return await Task.Run(() =>
            {
                using var md5 = MD5.Create();
                using var stream = File.OpenRead(path);
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            });
        }

        internal static long Size(string path)
        {
            if (!File.Exists(path))
                return -1;

            return new FileInfo(path).Length;
        }

        internal static string ReadText(string path, string def)
        {
            try
            {
                return File.ReadAllText(path) ?? def;
            }
            catch { }
            return def;
        }

        internal static void WriteText(string path, string data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    sw.Write(data);
            }
            catch { }
        }

        internal static T ReadJson<T>(string path, T def)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path)) ?? def;
            }
            catch { }
            return def;
        }

        internal static void WriteJson(string path, object obj)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
                    sw.Write(JsonConvert.SerializeObject(obj, Formatting.Indented));
            }
            catch { }
        }

        internal static bool Contains(string path, string text)
        {
            try
            {
                return File.ReadAllText(path).Contains(text);
            }
            catch { }
            return false;
        }

        internal static void Link(string path, string to)
        {
            if (File.Exists(path))
                File.Delete(path);

            CreateHardLink(path, to, IntPtr.Zero);
        }
    }
}
