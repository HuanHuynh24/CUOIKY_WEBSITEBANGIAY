using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Comment
    {
        public string MaComment { get; set; } = null!;
        public string? NoiDung { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? MaCmCha { get; set; }
        public string Taikhoan { get; set; } = null!;
        public string MaBlog { get; set; } = null!;

        public virtual Blog MaBlogNavigation { get; set; } = null!;
        public virtual User TaikhoanNavigation { get; set; } = null!;
    }
}
