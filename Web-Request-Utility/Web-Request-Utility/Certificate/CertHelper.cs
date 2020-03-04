using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Web_Request_Utility.File;
using Web_Request_Utility.String;

namespace Web_Request_Utility.Certificate
{
    public class CertHelper
    {

        public static X509Certificate2 LoadCertificate(string path, string password)
        {
            byte[] rawData = FileHelper.ReadFile(path);
            if (rawData == null || (uint) rawData.Length <= 0U)
            {
                return null;
            }

            if (string.IsNullOrEmpty(password))
            {
                return new X509Certificate2(rawData);
            }
            return new X509Certificate2(rawData, StringHelper.ConvertToSecureString(password), X509KeyStorageFlags.DefaultKeySet);
        }

    }
}
