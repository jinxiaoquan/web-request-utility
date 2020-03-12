using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Web_Request_Utility.Hash;

namespace Web_Request_Utility.HttpRequest
{
    public class Post
    {
        public static void Run(string url, string signStr, string key, string value, string fileName, FileStream fs)
        {
            var fileContent = new StreamContent(fs);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = $"\"{fileName}\"",
                FileName = $"\"{fileName}\""
            };
            var multiPart = new MultipartFormDataContent {{new StringContent(value), $"\"{key}\""}, fileContent};

            var request = new HttpRequestMessage(HttpMethod.Post, url) {Content = multiPart};
            request.Headers.Add("bearer", signStr);
            request.Headers.Add("Content-MD5", HashHelper.GetSha1Hash(fs));
            var client = new HttpClient();
            var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            var content = response.Content.ReadAsStringAsync().Result;
        }
    }
}
