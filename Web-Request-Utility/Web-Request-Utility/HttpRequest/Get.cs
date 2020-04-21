using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web_Request_Utility.ConvertExtensions;

namespace Web_Request_Utility.HttpRequest
{
    public class Get
    {
        public static void Run(string url, string signStr)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("bearer", signStr);
            var client = new HttpClient();
            var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
            var content = response.Content.ReadAsStringAsync().Result;
        }

        public static void Run1(string url, string domain, string userName, string password)
        {
            var endpointRequest = WebRequest.Create(url);
            endpointRequest.Method = "GET";
            endpointRequest.ContentType = "application/json";
            endpointRequest.Credentials = new NetworkCredential(userName, password, domain);
            try
            {
                var webResponse = endpointRequest.GetResponse();
                var webStream = webResponse.GetResponseStream();
                if (webStream != null)
                {
                    var responseReader = new StreamReader(webStream);
                    var response = responseReader.ReadToEnd();
                    Console.WriteLine(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }

        public static void Run2(string method, string url, string body, string accessToken)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Method = method;
            request.Proxy = new WebProxy("address", true);
            request.Proxy.Credentials = new NetworkCredential
            {
                UserName = "user",
                Password = "pwd"
            };
            if (!string.IsNullOrEmpty(body))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    var buffer = Encoding.UTF8.GetBytes(body);
                    requestStream.Write(buffer, 0, buffer.Length);
                }
            }

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.NoContent)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        Stream deCompressedStream = stream;
                        if (response.ContentEncoding == "gzip")
                        {
                            deCompressedStream = stream.GetDecompressGZip();
                        }
                        var resultString = new StreamReader(deCompressedStream).ReadToEnd();
                        
                    }
                }
            }
        }
    }
}
