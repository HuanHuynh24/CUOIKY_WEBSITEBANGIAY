using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Chitietsanpham
    {
        public Chitietsanpham()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        public string IdChitietSp { get; set; } = null!;
        public string? HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public decimal? Gia { get; set; }
        public int MaMau { get; set; }
        public string MaSanpham { get; set; } = null!;
        public int MaSize { get; set; }

        public virtual Color MaMauNavigation { get; set; } = null!;
        public virtual Sanpham MaSanphamNavigation { get; set; } = null!;
        public virtual Size MaSizeNavigation { get; set; } = null!;
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
