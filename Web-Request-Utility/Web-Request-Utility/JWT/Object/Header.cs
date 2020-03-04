using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Request_Utility.JWT.Object
{
    public class Header
    {
        [JsonProperty(PropertyName = "alg")]
        public string Alg { get; set; }

        [JsonProperty(PropertyName = "typ")]
        public string Typ { get; set; }

        public override string ToString()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(this)));
        }
    }
}
