using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Review
    {
        public string MaReview { get; set; } = null!;
        public string? NoiDung { get; set; }
        public DateTime? NgayNhap { get; set; }
        public int SoSao { get; set; }
        public string Taikhoan { get; set; } = null!;
        public string MaSanpham { get; set; } = null!;

        public virtual Sanpham MaSanphamNavigation { get; set; } = null!;
        public virtual User TaikhoanNavigation { get; set; } = null!;
    }
}
