using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class ChiTietSanPhamH
    {
        [JsonProperty("hinhAnh")]
        public string HinhAnh { get; set; }

        [JsonProperty("soLuongTon")]
        public int SoLuongTon { get; set; }

        [JsonProperty("gia")]
        public decimal Gia { get; set; }

        [JsonProperty("tenMau")]
        public string TenMau { get; set; }
    }
}