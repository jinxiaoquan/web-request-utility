using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.File
{
    public class FileHelper
    {

        public static byte[] ReadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return null;
            }
            using (Stream stream = new FileStream(filePath, FileMode.Open))
            {
                int count = (int)stream.Length;
                byte[] buffer = new byte[count];
                stream.Read(buffer, 0, count);
                return buffer;
            }
        }
    }
}
