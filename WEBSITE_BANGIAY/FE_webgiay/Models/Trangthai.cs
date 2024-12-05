﻿using System;
using System.Collections.Generic;

namespace FE_webgiay.Models
{
    public partial class Trangthai
    {
        public Trangthai()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public int Id { get; set; }
        public string? TenTrangthai { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
