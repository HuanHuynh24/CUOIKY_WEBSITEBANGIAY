using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Chitietsanphams = new HashSet<Chitietsanpham>();
            Reviews = new HashSet<Review>();
        }

        public string MaSanpham { get; set; } = null!;
        public string? TenSanpham { get; set; }
        public string? MoTa { get; set; }
        public int SoLuongyeuthich { get; set; }
        public int MaNhan { get; set; }
        public int MaDanhmuc { get; set; }
        public string MaKhuyenmai { get; set; } = null!;

        public virtual Danhmuc MaDanhmucNavigation { get; set; } = null!;
        public virtual Khuyenmai MaKhuyenmaiNavigation { get; set; } = null!;
        public virtual Nhanhieu MaNhanNavigation { get; set; } = null!;
        public virtual ICollection<Chitietsanpham> Chitietsanphams { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
