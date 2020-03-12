using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.Hash
{
    public class HashHelper
    {
        public static string GetSha1Hash(Stream stream)
        {
            stream.Position = 0;
            using (var sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(stream);
                stream.Position = 0;
                return Convert.ToBase64String(hash);
            }
        }
    }
}
