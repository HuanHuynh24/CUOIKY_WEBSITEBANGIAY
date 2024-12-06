using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class BrandDH
    {
        [JsonProperty("MaNhan")]
        public int MaNhan { get; set; }
        [JsonProperty("TenNhan")]
        public string TenNhan { get; set; }
    }
}