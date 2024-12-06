using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class ChiTietSanPhamDH
    {
        [JsonProperty("idChiTiet")]
        public int IdChiTiet { get; set; }
        [JsonProperty("hinhAnh")]
        public string HinhAnh { get; set; }
        [JsonProperty("SoLuongCon")]
        public int SoLuongCon { get; set; }
        [JsonProperty("Gia")]
        public decimal Gia { get; set; }
        [JsonProperty("MaMau")]
        public int MaMau { get; set; }
        [JsonProperty("maSize")]
        public int MaSize { get; set; }       

    }
}