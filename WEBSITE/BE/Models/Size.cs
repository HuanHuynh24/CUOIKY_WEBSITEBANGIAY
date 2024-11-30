using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class Size
    {
        public Size()
        {
            Chitietsanphams = new HashSet<Chitietsanpham>();
        }

        public int MaSize { get; set; }
        public double? Chieudai { get; set; }
        public double? Chieurong { get; set; }
        public double? Chieucao { get; set; }

        public virtual ICollection<Chitietsanpham> Chitietsanphams { get; set; }
    }
}
