using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class GioHangItem
    {
        public string IdCthd { get; set; }  // ID chi tiết hóa đơn
        public string IdHD { get; set; }    // ID hóa đơn
        public int SoLuong { get; set; }    // Số lượng
        public string HinhAnh { get; set; } // Hình ảnh sản phẩm
        public decimal Gia { get; set; }    // Giá sản phẩm
        public string TenSanPham { get; set; } // Tên sản phẩm
        public string Mau { get; set; }     
        public string Soluongton { get; set; }
        public int Makhuyenmai { get; set; }
        
    }

}