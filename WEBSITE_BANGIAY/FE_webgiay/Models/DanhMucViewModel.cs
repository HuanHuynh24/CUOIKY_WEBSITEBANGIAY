namespace FE_webgiay.Models
{
    public class DanhMucViewModel
    {
        public IEnumerable<Danhmuc> DanhMucs { get; set; } // Danh sách danh mục
        public Danhmuc NewDanhMuc { get; set; }            // Dữ liệu danh mục mới
    }
}
