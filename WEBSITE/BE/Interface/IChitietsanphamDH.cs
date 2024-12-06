using BE.Models;

namespace BE.Interface
{
    public interface IChitietsanphamDH
    {
        // Lấy tất cả chi tiết sản phẩm
        Task<IEnumerable<Chitietsanpham>> GetAllChitietsanphams();

        // Lấy danh sách chi tiết sản phẩm theo khoảng giá
        Task<IEnumerable<Chitietsanpham>> GetChitietsanphamsByPriceRange(decimal minPrice, decimal maxPrice);
        
        // Lọc theo màu
        Task<IEnumerable<Chitietsanpham>> GetByColor(int maMau);

        // Lọc theo nhãn hiệu
        Task<IEnumerable<Chitietsanpham>> GetChitietsanphamByNhanhieu(int maNhan);
        // Lọc chi tiết sản phẩm theo danh mục
        Task<IEnumerable<Chitietsanpham>> GetChitietsanphamByDanhmuc(int maDanhMuc);
    }
}
