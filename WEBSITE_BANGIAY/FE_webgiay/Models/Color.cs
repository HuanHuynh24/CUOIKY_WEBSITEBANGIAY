﻿using System;
using System.Collections.Generic;

namespace FE_webgiay.Models
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