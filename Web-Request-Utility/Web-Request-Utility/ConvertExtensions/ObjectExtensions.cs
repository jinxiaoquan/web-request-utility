using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Web_Request_Utility.ConvertExtensions
{
    public static class ObjectExtensions
    {
        public static byte[] Convert2Bytes(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            byte[] buff;
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter iFormatter = new BinaryFormatter();
                iFormatter.Serialize(ms, obj);
                buff = ms.GetBuffer();
            }
            return buff;
        }

        public static string Convert2Json(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj);
        }
    }
}
