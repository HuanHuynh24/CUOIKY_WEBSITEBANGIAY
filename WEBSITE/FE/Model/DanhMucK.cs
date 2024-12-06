using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class DanhmucK
    {
        public string TenDanhMuc { get; set; }

        public int SoLuongBlog { get; set; }
        public List<BlogK> Blogs { get; set; }
    }
}