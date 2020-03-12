using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
