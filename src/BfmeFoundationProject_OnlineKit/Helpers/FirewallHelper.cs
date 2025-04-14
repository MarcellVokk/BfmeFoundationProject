using System.Diagnostics;

namespace BfmeFoundationProject.OnlineKit.Helpers
{
    public static class FirewallHelper
    {
        public static void AddFirewallRule(string name, string program)
        {
            if (!FirewallRuleExists(name + " [in tcp]"))
            {
                var proc1 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [in tcp]\" dir=in action=allow program=\"{program}\" enable=yes protocol=TCP", CreateNoWindow = true });
                proc1?.WaitForExit();
            }

            if (!FirewallRuleExists(name + " [in udp]"))
            {
                var proc1 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [in udp]\" dir=in action=allow program=\"{program}\" enable=yes protocol=UDP", CreateNoWindow = true });
                proc1?.WaitForExit();
            }

            if (!FirewallRuleExists(name + " [in]"))
            {
                var proc1 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [in]\" dir=in action=allow program=\"{program}\" enable=yes", CreateNoWindow = true });
                proc1?.WaitForExit();
            }

            if (!FirewallRuleExists(name + " [out tcp]"))
            {
                var proc2 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [out tcp]\" dir=out action=allow program=\"{program}\" enable=yes protocol=TCP", CreateNoWindow = true });
                proc2?.WaitForExit();
            }

            if (!FirewallRuleExists(name + " [out udp]"))
            {
                var proc2 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [out udp]\" dir=out action=allow program=\"{program}\" enable=yes protocol=UDP", CreateNoWindow = true });
                proc2?.WaitForExit();
            }

            if (!FirewallRuleExists(name + " [out]"))
            {
                var proc2 = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall add rule name=\"{name} [out]\" dir=out action=allow program=\"{program}\" enable=yes", CreateNoWindow = true });
                proc2?.WaitForExit();
            }
        }

        private static bool FirewallRuleExists(string ruleName)
        {
            string output = "";

            var proc = Process.Start(new ProcessStartInfo { FileName = "netsh", Arguments = $"advfirewall firewall show rule name=\"{ruleName}\"", CreateNoWindow = true, RedirectStandardOutput = true });
            if (proc != null)
                proc.OutputDataReceived += (s, e) => output += e.Data ?? "";
            proc?.BeginOutputReadLine();
            proc?.WaitForExit();

            return output.Contains(ruleName);
        }
    }
}
