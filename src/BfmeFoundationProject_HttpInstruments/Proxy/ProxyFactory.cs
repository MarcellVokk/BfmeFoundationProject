using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace BfmeFoundationProject.HttpInstruments.Proxy
{
    public class ProxyFactory(ProxyConfiguration? conf)
    {
        public IWebProxy? CreateProxy()
        {
            var validationErrors = ProxyConfigurationValidator.Validate(conf).ToArray();

            if (validationErrors.Length > 0)
            {
                var errorsTexts = validationErrors
                    .Select(x => $"Field: {x.MemberNames.FirstOrDefault() ?? "NULL"} - {x.ErrorMessage ?? "NULL"}")
                    .ToArray();
                var errorMessage = string.Join(Environment.NewLine, errorsTexts);
                throw new BfmeProxyException(errorMessage);
            }
           
            if (conf.ProxyType is ProxyType.None)
            {
                return null;
            }
            if (conf.ProxyType is ProxyType.System)
            {
                return HttpClient.DefaultProxy;
            }
            if (TryFormatUri(conf.Url, conf.ProxyType, out var uri))
            {
                return new WebProxy(
                    uri, 
                    conf.BypassLocal, 
                    conf.Bypasses, 
                    CreateCredentials(conf));
            }
            return null;
        }

        private static bool TryFormatUri(string url, ProxyType type, [NotNullWhen(true)] out Uri? uri)
        {
            uri = null;

            if (string.IsNullOrEmpty(url)) { return false; }
            if (type is ProxyType.None or ProxyType.System) { return false; }

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var confUri))
            {
                if (IsTypeConfigured(confUri))
                {
                    uri = confUri;
                    return true;
                }
                var uriBuilder = new UriBuilder(confUri)
                {
                    Scheme = GetSchemaForProxyType(type)
                };
                uri = uriBuilder.Uri;
                return true;
            }

            return false;
        }

        private static bool IsTypeConfigured(Uri url)
        {
            return Enum.GetValues<ProxyType>()
                    .Select(x => GetSchemaForProxyType(x))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Any(x => url.Scheme.Equals(x, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetSchemaForProxyType(ProxyType type)
        {
            return type switch
            {
                ProxyType.Http => "http",
                ProxyType.Https => "https",
                ProxyType.Socks4 => "socks4",
                ProxyType.Socks4Domain => "socks4a",
                ProxyType.Socks5 => "socks5",
                ProxyType.Socks5Domain => "socks5a",
                _ => string.Empty,
            };
        }

        private static NetworkCredential? CreateCredentials(ProxyConfiguration conf)
        {
            if (!conf.UseAuthorization)
            {
                return null;
            }
            return new NetworkCredential(conf.Username, conf.Password, conf.Domain);
        }
    }
}
