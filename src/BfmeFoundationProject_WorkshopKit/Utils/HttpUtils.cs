using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using BfmeFoundationProject.HttpInstruments;
using BfmeFoundationProject.WorkshopKit.Data;
using BfmeFoundationProject.WorkshopKit.Logic;
using System.Collections.Specialized;

namespace BfmeFoundationProject.WorkshopKit.Utils
{
    internal static class HttpUtils
    {
        internal static async Task<string> GetString(BfmeWorkshopAuthInfo authInfo, string apiEndpointPath, Dictionary<string, string>? requestParameters = null)
        {
            NameValueCollection requestQueryParameters = System.Web.HttpUtility.ParseQueryString(string.Empty);
            requestParameters?.ToList().ForEach(x => requestQueryParameters.Add(x.Key, x.Value == "" ? "~" : x.Value));

            return await HttpMarshal.GetString(
            url: $"{BfmeWorkshopManager.WorkshopServerHost}/api/{apiEndpointPath}{(requestQueryParameters.Count > 0 ? $"?{requestQueryParameters.ToString()}" : "")}",
            headers: new Dictionary<string, string>() { { "AuthAccountUuid", authInfo.Uuid }, { "AuthAccountPassword", authInfo.Password } });
        }

        internal static async Task<T?> GetJson<T>(BfmeWorkshopAuthInfo authInfo, string apiEndpointPath, Dictionary<string, string>? requestParameters = null)
        {
            NameValueCollection requestQueryParameters = System.Web.HttpUtility.ParseQueryString(string.Empty);
            requestParameters?.ToList().ForEach(x => requestQueryParameters.Add(x.Key, x.Value == "" ? "~" : x.Value));

            return await HttpMarshal.GetJson<T>(
            url: $"{BfmeWorkshopManager.WorkshopServerHost}/api/{apiEndpointPath}{(requestQueryParameters.Count > 0 ? $"?{requestQueryParameters.ToString()}" : "")}",
            headers: new Dictionary<string, string>() { { "AuthAccountUuid", authInfo.Uuid }, { "AuthAccountPassword", authInfo.Password } });
        }

        internal static async Task<string> SetString(BfmeWorkshopAuthInfo authInfo, string apiEndpointPath, string data)
        {
            return await HttpMarshal.PostString(
            url: $"{BfmeWorkshopManager.WorkshopServerHost}/api/{apiEndpointPath}",
            data: data,
            headers: new Dictionary<string, string>() { { "AuthAccountUuid", authInfo.Uuid }, { "AuthAccountPassword", authInfo.Password } });
        }

        internal static async Task Upload(BfmeWorkshopAuthInfo authInfo, string source, string id = "", Action<int>? OnProgressUpdate = null)
        {
            using var workshop_files = new AmazonS3Client("b27b3b7fd5c041cd488270b978fc83a1", "927174808adede24038b5acdac2ca386bfa8ae02b245d57f46e2a2b76f9e2c92", new AmazonS3Config { ServiceURL = $"https://32d721493580a1de3fa984779848a30d.r2.cloudflarestorage.com" });
            using var transfer_utility = new TransferUtility(workshop_files);

            var request = new TransferUtilityUploadRequest();
            request.BucketName = "workshop-files";
            request.Key = $"{authInfo.Uuid}/{id}";
            request.InputStream = new FileStream(source, FileMode.Open, FileAccess.Read);
            request.ContentType = "application/octet-stream";
            request.DisablePayloadSigning = true;
            request.PartSize = 6291456;

            int lastProgress = 0;
            request.UploadProgressEvent += (s, e) =>
            {
                if (e.PercentDone != lastProgress)
                {
                    OnProgressUpdate?.Invoke(e.PercentDone);
                    lastProgress = e.PercentDone;
                }
            };

            await transfer_utility.UploadAsync(request);
        }

        internal static async Task Delete(BfmeWorkshopAuthInfo authInfo, string[] ids)
        {
            using var workshop_files = new AmazonS3Client("b27b3b7fd5c041cd488270b978fc83a1", "927174808adede24038b5acdac2ca386bfa8ae02b245d57f46e2a2b76f9e2c92", new AmazonS3Config { ServiceURL = $"https://32d721493580a1de3fa984779848a30d.r2.cloudflarestorage.com" });

            foreach (var group in ids.Select((x, i) => (x, i)).GroupBy(x => x.i / 1000).Select(x => x.Select(x => x.x)))
            {
                var request = new DeleteObjectsRequest();
                request.BucketName = "workshop-files";
                foreach (var id in group) request.AddKey(id);

                await workshop_files.DeleteObjectsAsync(request);
            }
        }

        internal static async Task<string> Delete(BfmeWorkshopAuthInfo authInfo, string apiEndpointPath, string id = "")
        {
            NameValueCollection requestQueryParameters = System.Web.HttpUtility.ParseQueryString(string.Empty);
            if (id != "") requestQueryParameters.Add("id", id);

            return await HttpMarshal.Delete(
            url: $"{BfmeWorkshopManager.WorkshopServerHost}/api/{apiEndpointPath}?{requestQueryParameters.ToString()}",
            headers: new Dictionary<string, string>() { { "AuthAccountUuid", authInfo.Uuid }, { "AuthAccountPassword", authInfo.Password } });
        }
    }
}
