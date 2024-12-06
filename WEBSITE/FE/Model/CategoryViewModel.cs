using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FE.Model
{
    public class CategoryViewModel
    {
        public List<DanhMucDH> DanhMucs { get; set; }
        public List<BrandDH> Brands { get; set; }
        public List<ColorDH> Colors { get; set; }
        public List<ChiTietSanPhamDH> SanPhams { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}