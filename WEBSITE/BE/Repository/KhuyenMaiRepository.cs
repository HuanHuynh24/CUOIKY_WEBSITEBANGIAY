using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BE.Repository
{
    public class KhuyenMaiRepository
    {
        private readonly db_websitebanhangContext _context;
        public KhuyenMaiRepository(db_websitebanhangContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Khuyenmai>> GetKhuyenmais()
        {
            return await _context.Khuyenmais.ToListAsync();
        }
        public async Task<Khuyenmai> GetKhuyenmai(string id)
        {
            return await _context.Khuyenmais.FirstOrDefaultAsync(e => e.MaKhuyenmai == id);
        }

        public async Task<Khuyenmai> AddKhuyenmai([FromBody] Khuyenmai khuyenmai)
        {

            // Tạo một đối tượng Danhmuc mới từ tham số truyền vào
            var khuyenmainew = new Khuyenmai
            {
                MaKhuyenmai = khuyenmai.MaKhuyenmai,
                TenKhuyenmai = khuyenmai.TenKhuyenmai,
                PhanTram = khuyenmai.PhanTram

            };

            // Thêm vào DbContext
            _context.Khuyenmais.Add(khuyenmainew);
            await _context.SaveChangesAsync();

            // Trả về kết quả đã tạo
            return khuyenmainew;
        }

        public async Task<Khuyenmai?> UpdateKhuyenmai(string id, [FromBody] Khuyenmai updatedKhuyenmai)
        {
            // Tìm danh mục theo ID
            var existingKhuyenmai = await _context.Khuyenmais.FindAsync(id);

            if (existingKhuyenmai == null)
            {
                return null; // Nếu không tồn tại, trả về null
            }

            // Chỉ cập nhật TenDanhmuc từ updatedDanhmuc
            existingKhuyenmai.TenKhuyenmai = updatedKhuyenmai.TenKhuyenmai;
            existingKhuyenmai.PhanTram = updatedKhuyenmai.PhanTram;

            // Lưu thay đổi
            _context.Khuyenmais.Update(existingKhuyenmai);
            await _context.SaveChangesAsync();

            return existingKhuyenmai;
        }

        public async Task<int> DeleteKhuyenmai(string id)
        {
            var khuyenmai = await _context.Khuyenmais.FindAsync(id);
            if (khuyenmai == null)
            {
                return 0;
            }

            _context.Khuyenmais.Remove(khuyenmai);
            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
