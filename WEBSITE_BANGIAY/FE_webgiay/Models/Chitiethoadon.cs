using System;
using System.Collections.Generic;

namespace FE_webgiay.Models
{
    public partial class Chitiethoadon
    {
        public string IdChitietHd { get; set; } = null!;
        public string? MaHoadon { get; set; }
        public string? IdChitietSp { get; set; }
        public int? SoLuong { get; set; }

        public virtual Chitietsanpham? IdChitietSpNavigation { get; set; }
        public virtual Hoadon? MaHoadonNavigation { get; set; }
    }
}
