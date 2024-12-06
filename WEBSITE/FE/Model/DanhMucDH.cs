using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class DanhMucDH
    {
        [JsonProperty("MaDanhMuc")]
        public int MaDanhMuc { get; set; }
        [JsonProperty("TenDanhMuc")]
        public string TenDanhMuc { get; set; }
    }
}