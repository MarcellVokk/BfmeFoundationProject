using System.ComponentModel.DataAnnotations;

namespace BfmeFoundationProject.HttpInstruments.Proxy
{
    public sealed record ProxyConfiguration
    {
        public const string ProxyConfigFileName = "proxy-configuration.json";

        public ProxyType ProxyType { get; init; } = ProxyType.None;
        
        public string Url { get; init; } = string.Empty;

        public string[] Bypasses { get; init; } = [];

        public bool BypassLocal { get; init; } = true;

        public bool UseAuthorization { get; init; } = false;
        public string Username { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string? Domain { get; init; }
    }

    public static class ProxyConfigurationValidator
    {
        public static IEnumerable<ValidationResult> Validate(ProxyConfiguration? conf)
        {
            if (conf == null)
            {
                yield return new ValidationResult("The proxy configuration is null!", [nameof(ProxyConfiguration)]);
                yield break;
            }
            if (conf.ProxyType is ProxyType.None or ProxyType.System)
            {
                yield break;
            }
            foreach(var networkErrors in ValidateNetworkSettings(conf))
            {
                yield return networkErrors;
            }
            if (conf.UseAuthorization)
            {
                foreach(var credErrors in ValidateCreadentials(conf))
                {
                    yield return credErrors;
                }
            }
        }

        private static IEnumerable<ValidationResult> ValidateNetworkSettings(ProxyConfiguration conf)
        {
            if (string.IsNullOrWhiteSpace(conf.Url))
            {
                yield return new ValidationResult("The url is empty!", [nameof(ProxyConfiguration.Url)]);
            }
            if (!Uri.TryCreate(conf.Url, UriKind.Absolute, out _))
            {
                yield return new ValidationResult("The url is incorrect! The absolute url is expected", [nameof(ProxyConfiguration.Url)]);
            }
            if (conf.Bypasses is null)
            {
                yield return new ValidationResult("The bypasses is null!", [nameof(ProxyConfiguration.Bypasses)]);
                yield break;
            }
            foreach(var bps in conf.Bypasses)
            {
                if (string.IsNullOrWhiteSpace(bps))
                {
                    yield return new ValidationResult("Contains empty value!", [nameof(ProxyConfiguration.Bypasses)]);
                    yield break;
                }
            }
        }

        private static IEnumerable<ValidationResult> ValidateCreadentials(ProxyConfiguration conf)
        {
            if (string.IsNullOrWhiteSpace(conf.Username))
            {
                yield return new ValidationResult("The username is empty!", [nameof(ProxyConfiguration.Username)]);
            }
            if (string.IsNullOrWhiteSpace(conf.Password))
            {
                yield return new ValidationResult("The password is empty!", [nameof(ProxyConfiguration.Password)]);
            }
        }
    }

}
