using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class NhanhieuH
    {
        [JsonProperty("tenNhanHieu")]
        public string tenNhanHieu { get; set; }

        [JsonProperty("sanphams")]
        public List<SanphamH> sanphamList { get; set; }

      
    }
}