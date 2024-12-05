using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Danhmuc
    {
        public Danhmuc()
        {
            Blogs = new HashSet<Blog>();
            Sanphams = new HashSet<Sanpham>();
        }

        public int MaDanhmuc { get; set; }
        public string? TenDanhmuc { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
