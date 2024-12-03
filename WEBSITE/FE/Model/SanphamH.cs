using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class SanphamH
    {
        [JsonProperty("tenSanpham")]
        public string TenSanPham { get; set; }

        [JsonProperty("moTa")]
        public string MoTa { get; set; }

        [JsonProperty("chitietsanpham")]
        public List<ChiTietSanPhamH> ChiTietSanPhamList { get; set; }

       

        [JsonProperty("phantramkhuyenmai")]
        public int PhanTramKhuyenMai { get; set; }
    }
}