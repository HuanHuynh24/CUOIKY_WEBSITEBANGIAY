using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Khuyenmai
    {
        public Khuyenmai()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public string MaKhuyenmai { get; set; } = null!;
        public string? TenKhuyenmai { get; set; }
        public double? PhanTram { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
