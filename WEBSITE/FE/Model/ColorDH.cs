using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class ColorDH
    {
        [JsonProperty("MaMau")]
        public int MaMau { get; set; }
        [JsonProperty("TenMau")]
        public string TenMau { get; set; }
    }
}