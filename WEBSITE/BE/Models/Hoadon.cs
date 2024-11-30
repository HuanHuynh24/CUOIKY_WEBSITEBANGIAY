using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        public string MaHoadon { get; set; } = null!;
        public string? DiaChi { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayGiao { get; set; }
        public int IdTrangthai { get; set; }
        public decimal? TongTien { get; set; }
        public string Taikhoan { get; set; } = null!;

        public virtual Trangthai IdTrangthaiNavigation { get; set; } = null!;
        public virtual User TaikhoanNavigation { get; set; } = null!;
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
