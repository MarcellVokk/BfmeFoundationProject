namespace BfmeFoundationProject.HttpInstruments.Proxy
{
    public class BfmeProxyException(string message, Exception? inner = null) : Exception(message, inner)
    { }
}
