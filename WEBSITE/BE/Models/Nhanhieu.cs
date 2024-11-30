using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Nhanhieu
    {
        public Nhanhieu()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int MaNhan { get; set; }
        public string? TenNhan { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
