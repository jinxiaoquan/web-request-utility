using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Web_Request_Utility.ConvertExtensions
{
    public static class ByteArrayExtensions
    {
        public static object Convert2Object(this byte[] buff)
        {
            if (buff == null)
            {
                return null;
            }
            object obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return obj;
        }

        public static T Convert2Object<T>(this byte[] buff)
        {
            if (buff == null)
            {
                return default(T);
            }
            object obj;
            using (MemoryStream ms = new MemoryStream(buff))
            {
                IFormatter iFormatter = new BinaryFormatter();
                obj = iFormatter.Deserialize(ms);
            }
            return (T)obj;
        }


        public static string Convert2String(this byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
            string result = Encoding.UTF8.GetString(bytes);
            Array.Clear(bytes, 0, bytes.Length);
            return result;
        }
    }
}
