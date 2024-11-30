using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Blog
    {
        public Blog()
        {
            Comments = new HashSet<Comment>();
        }

        public string MaBlog { get; set; } = null!;
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public DateTime? Ngaytao { get; set; }
        public string? Hinhanh { get; set; }
        public int MaDanhmuc { get; set; }

        public virtual Danhmuc MaDanhmucNavigation { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
