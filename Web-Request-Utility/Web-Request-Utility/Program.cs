using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Web_Request_Utility.Certificate;
using Web_Request_Utility.JWT;

namespace Web_Request_Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            var pfxPath = @"c:\test.pfx";
            var pfx = CertHelper.LoadCertificate(pfxPath, "123");
            var iss = "Hello World";
            var signResult = JwtHelper.SignStr(iss, pfx, "SHA256");

            var cerPah = @"c:\test.cer";
            var cer = CertHelper.LoadCertificate(cerPah);
            var result = JwtHelper.Verify(signResult, cer, "SHA256");

            Console.WriteLine("Finish, verify : {0}", result);
            Console.ReadLine();
        }

        static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

    }
}
 