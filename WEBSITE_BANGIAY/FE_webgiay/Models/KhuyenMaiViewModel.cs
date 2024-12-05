namespace FE_webgiay.Models
{
    public class KhuyenMaiViewModel
    {
        public IEnumerable<Khuyenmai> KhuyenMais { get; set; } // Danh sách khuyến mãi
        public Khuyenmai NewKhuyenMai { get; set; }            // Dữ liệu khuyến mãi mới
    }
}
