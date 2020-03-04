using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Web_Request_Utility.JWT.Object
{
    public class Payload
    {
        [JsonProperty(PropertyName = "iss")]
        public string Iss { get; set; }

        [JsonProperty(PropertyName = "jti")]
        public string Jti { get; set; }

        [JsonProperty(PropertyName = "exp")]
        public long Exp { get; set; }

        public override string ToString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
        }
    }
}
