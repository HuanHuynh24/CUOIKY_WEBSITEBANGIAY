using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Chitiethoadon
    {
        public string IdChitietHd { get; set; } = null!;
        public string? MaHoadon { get; set; }
        public string? IdChitietSp { get; set; }
        public int? SoLuong { get; set; }
        public int? TrangThai { get; set; }

        public virtual Chitietsanpham? IdChitietSpNavigation { get; set; }
        public virtual Hoadon? MaHoadonNavigation { get; set; }
    }
}
