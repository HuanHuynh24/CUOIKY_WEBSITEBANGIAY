namespace FE_webgiay.Models
{
    public class NhanHieuViewModel
    {
        public IEnumerable<Nhanhieu> NhanHieus { get; set; } // Danh sách thương hiệu
        public Nhanhieu NewNhanHieu { get; set; }            // Dữ liệu thương hiệu mới
    }
}
