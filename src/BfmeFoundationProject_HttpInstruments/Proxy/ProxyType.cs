using System.Text.Json.Serialization;

namespace BfmeFoundationProject.HttpInstruments.Proxy
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProxyType
    {
        None = 1,
        System = 2,
        Http = 3,
        Https = 4,
        Socks4 = 5,
        Socks4Domain = 6,
        Socks5 = 7,
        Socks5Domain = 8,

    }
}
