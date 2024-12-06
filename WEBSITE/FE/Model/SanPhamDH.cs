using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class SanPhamDH
    {
        [JsonProperty("MaSanpham")]
        public string MaSanPham { get; set; }
        [JsonProperty("TenSanpham")]
        public string TenSanPham { get; set; }

        [JsonProperty("MoTa")]
        public string MoTa { get; set; }

        [JsonProperty("phantramkhuyenmai")]
        public int PhanTramKhuyenMai { get; set; }
    }
}