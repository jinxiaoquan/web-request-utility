using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.ConvertExtensions
{
    public static class StreamExtensions
    {
        public static Stream GetDecompressGZip(this Stream stream)
        {
            var deCompressedStream = new MemoryStream();
            using (var gzStream = new GZipStream(stream, CompressionMode.Decompress))
            {
                var buffer = new byte[1024];
                int nRead;
                while ((nRead = gzStream.Read(buffer, 0, buffer.Length))> 0)
                {
                    deCompressedStream.Write(buffer, 0, nRead);
                }
            }
            deCompressedStream.Seek(0, SeekOrigin.Begin);
            return deCompressedStream;
        }
    }
}
