using Newtonsoft.Json;
using System.IO.Compression;
using System.Net.Http.Handlers;
using System.Text;

namespace BfmeFoundationProject.HttpInstruments
{
    public class HttpMarshal
    {
        private static SocketsHttpHandler SocketsHandler = new SocketsHttpHandler()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
            MaxConnectionsPerServer = 10
        };

        public static async Task<string> GetString(string url, Dictionary<string, string> headers)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    using var client = new HttpClient(SocketsHandler, disposeHandler: false);

                    client.Timeout = TimeSpan.FromSeconds(90);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                    using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                    foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseString) && response.Content.Headers.Contains("Content-Encoding") && response.Content.Headers.GetValues("Content-Encoding").Contains("b64-gzip"))
                    {
                        var gZipBuffer = Convert.FromBase64String(responseString);
                        using var memoryStream = new MemoryStream();
                        using var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
                        int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                        var buffer = new byte[dataLength];

                        memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);
                        memoryStream.Position = 0;

                        int totalRead = 0;
                        while (totalRead < buffer.Length)
                        {
                            int bytesRead = gZipStream.Read(buffer, totalRead, buffer.Length - totalRead);
                            if (bytesRead == 0) break;
                            totalRead += bytesRead;
                        }

                        responseString = Encoding.UTF8.GetString(buffer);
                    }

                    return responseString;
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw new HttpRequestException($"GET failed! URL: {url}\n{ex.ToString()}");
                }
            }

            throw new HttpRequestException($"GET failed! URL: {url}\nBad behaviour");
        }

        public static async Task GetFile(string url, string localPath, Dictionary<string, string> headers, Action<int>? OnProgressUpdate = null)
        {
            for (int i = 1; i <= 5; i++)
            {
                // We don't need to dispose this, since all that would do is dispose the inner SocketsHandler, which we don't want...
                var progress_handler = new ProgressMessageHandler(SocketsHandler);

                try
                {
                    using var client = new HttpClient(progress_handler, disposeHandler: false);

                    client.Timeout = TimeSpan.FromHours(5);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                    int last_reported_progress = -1;
                    progress_handler.HttpReceiveProgress += (_, e) =>
                    {
                        if (last_reported_progress != e.ProgressPercentage)
                        {
                            last_reported_progress = e.ProgressPercentage;
                            OnProgressUpdate?.Invoke(e.ProgressPercentage);
                        }
                    };

                    using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                    foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                    using var filestream = new FileStream(localPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    using var netstream = response.Content.ReadAsStream();
                    await netstream.CopyToAsync(filestream);
                    break;
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw new HttpRequestException($"GET failed! URL: {url}\n{ex.ToString()}");

                    OnProgressUpdate?.Invoke(0);
                }
            }
        }

        public static async Task<T?> GetJson<T>(string url, Dictionary<string, string> headers)
        {
            return JsonConvert.DeserializeObject<T>(await GetString(url, headers));
        }

        public static async Task<string> PostString(string url, string data, Dictionary<string, string> headers)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    using var client = new HttpClient(SocketsHandler, disposeHandler: false);

                    client.Timeout = TimeSpan.FromSeconds(90);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                    using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
                    foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                    request.Content = new StringContent(data, Encoding.UTF8, "text/plain");
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw new HttpRequestException($"POST failed! URL: {url}\n{ex.ToString()}");
                }
            }

            throw new HttpRequestException($"POST failed! URL: {url}\nBad behaviour");
        }

        public static async Task PutFile(string url, string localPath, Dictionary<string, string> headers, Action<int>? OnProgressUpdate = null)
        {
            for (int i = 1; i <= 5; i++)
            {
                // We don't need to dispose this, since all that would do is dispose the inner SocketsHandler, which we don't want...
                var progress_handler = new ProgressMessageHandler(SocketsHandler);

                try
                {
                    using var client = new HttpClient(SocketsHandler, disposeHandler: false);

                    client.Timeout = TimeSpan.FromHours(5);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                    int last_reported_progress = -1;
                    progress_handler.HttpSendProgress += (_, e) =>
                    {
                        if (last_reported_progress != e.ProgressPercentage)
                        {
                            last_reported_progress = e.ProgressPercentage;
                            OnProgressUpdate?.Invoke(e.ProgressPercentage);
                        }
                    };

                    using var request = new HttpRequestMessage(HttpMethod.Put, new Uri(url));
                    foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                    using var filestream = new FileStream(localPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    request.Content = new StreamContent(filestream);
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    break;
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw new HttpRequestException($"PUT failed! URL: {url}\n{ex.ToString()}");
                }
            }
        }

        public static async Task<string> Delete(string url, Dictionary<string, string> headers)
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    using var client = new HttpClient(SocketsHandler, disposeHandler: false);

                    client.Timeout = TimeSpan.FromSeconds(90);
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

                    using var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
                    foreach (var header in headers) request.Headers.Add(header.Key, header.Value);
                    using var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    if (i == 5)
                        throw new HttpRequestException($"DELETE failed! URL: {url}\n{ex.ToString()}");
                }
            }

            throw new HttpRequestException($"DELETE failed! URL: {url}\nBad behaviour");
        }
    }
}
