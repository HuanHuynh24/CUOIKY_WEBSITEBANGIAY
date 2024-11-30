using System;
using System.Collections.Generic;

namespace BE.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Hoadons = new HashSet<Hoadon>();
            Reviews = new HashSet<Review>();
        }

        public string Taikhoan { get; set; } = null!;
        public string Matkhau { get; set; } = null!;
        public string? Ten { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
