using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

    }
}
