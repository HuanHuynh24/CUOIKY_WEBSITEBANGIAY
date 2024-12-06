using BE.Interface;
using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class ChitietsanphamDH : IChitietsanphamDH
    {
        private readonly db_websitebanhangContext _context; // Biến để kết nối với cơ sở dữ liệu

        public ChitietsanphamDH(db_websitebanhangContext context)
        {
            _context = context; // Inject DbContext từ Dependency Injection
        }

        // Trả về danh sách tất cả chi tiết sản phẩm
        public async Task<IEnumerable<Chitietsanpham>> GetAllChitietsanphams()
        {
            // Truy vấn dữ liệu từ bảng Chitietsanphams kèm thông tin từ bảng Sanpham
            return await _context.Chitietsanphams.ToListAsync();
        }

        // Trả về danh sách chi tiết sản phẩm theo khoảng giá
        public async Task<IEnumerable<Chitietsanpham>> GetChitietsanphamsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            // Truy vấn  chi tiết sản phẩm có giá nằm trong khoảng [minPrice, maxPrice]
            return await Task.FromResult(
                _context.Chitietsanphams.Where(ct => ct.Gia.HasValue && ct.Gia >= minPrice && ct.Gia <= maxPrice).ToList());
        }
        // Trả về danh sách chi tiết sản phẩm theo màu
        public async Task<IEnumerable<Chitietsanpham>> GetByColor(int maMau)
        {
            return await Task.FromResult(
                _context.Chitietsanphams.Where(c => c.MaMau == maMau).ToList());
        }
        // Trả về danh sách chi tiết sản phẩm theo nhãn hiệu
        public async Task<IEnumerable<Chitietsanpham>> GetChitietsanphamByNhanhieu(int maNhan)
        {
            // Thực hiện phép nối giữa Chitietsanpham và Sanpham
            var result = await _context.Chitietsanphams
                .Join(_context.Sanphams, // Nối với bảng Sanpham
                    ct => ct.MaSanpham,  // Điều kiện nối (MaSanpham trong Chitietsanpham)
                    sp => sp.MaSanpham,  // Điều kiện nối (MaSanpham trong Sanpham)
                    (ct, sp) => new { Chitiet = ct, Sanpham = sp })  // Lấy các đối tượng Chitietsanpham và Sanpham sau khi nối
                .Where(x => x.Sanpham.MaNhan == maNhan)  // Lọc theo MaNhan trong Sanpham
                .Select(x => x.Chitiet)  // Trả về chỉ Chitietsanpham sau khi đã lọc
                .ToListAsync();

            return result;
        }
        public async Task<IEnumerable<Chitietsanpham>> GetChitietsanphamByDanhmuc(int maDanhMuc)
        {
            var result = await _context.Chitietsanphams
                .Join(_context.Sanphams, // Nối bảng Chitietsanpham và Sanpham
                    ct => ct.MaSanpham,
                    sp => sp.MaSanpham,
                    (ct, sp) => new { Chitiet = ct, Sanpham = sp }) // Lấy Chitiet và Sanpham
                .Join(_context.Danhmucs, // Nối tiếp với bảng Danhmuc
                    sp => sp.Sanpham.MaDanhmuc,
                    dm => dm.MaDanhmuc,
                    (sp, dm) => new { Chitiet = sp.Chitiet, Danhmuc = dm }) // Lấy Chitiet và Danhmuc
                .Where(x => x.Danhmuc.MaDanhmuc == maDanhMuc) // Lọc theo mã danh mục
                .Select(x => x.Chitiet) // Trả về Chitietsanpham
                .ToListAsync();

            return result;
        }


    }
}
