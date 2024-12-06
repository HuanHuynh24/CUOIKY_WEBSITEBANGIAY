using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class BlogK
    {
        [Key]
        public int MaBlog { get; set; }
        public string Tieude { get; set; }
        public string NoidungTrichDan { get; set; }
        public string Noidung { get; set; }
        public DateTime Ngaytao { get; set; }
        public string Hinhanh { get; set; }
        public string Danhmuc { get; set; }
        public int CountCmt { get; set; }
        public List<CommentK> Comments { get; set; }
    }
}