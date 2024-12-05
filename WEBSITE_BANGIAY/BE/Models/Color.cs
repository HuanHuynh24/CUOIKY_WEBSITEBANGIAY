using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Color
    {
        public Color()
        {
            Chitietsanphams = new HashSet<Chitietsanpham>();
        }

        public int MaMau { get; set; }
        public string? TenMau { get; set; }

        public virtual ICollection<Chitietsanpham> Chitietsanphams { get; set; }
    }
}
