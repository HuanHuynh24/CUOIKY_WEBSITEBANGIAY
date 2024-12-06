using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BE.Repository
{
    public class ThuongHieuRepository
    {
        private readonly db_websitebanhangContext _context;
        public ThuongHieuRepository(db_websitebanhangContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Nhanhieu>> GetNhanhieus()
        {
            return await _context.Nhanhieus.ToListAsync();
        }
        public async Task<Nhanhieu> GetNhanhieu(int id)
        {
            return await _context.Nhanhieus.FirstOrDefaultAsync(e => e.MaNhan == id);
        }

        public async Task<Nhanhieu> AddNhanhieu([FromBody] Nhanhieu nhanhieu)
        {

            // Tạo một đối tượng Danhmuc mới từ tham số truyền vào
            var nhanhieunew = new Nhanhieu
            {
                MaNhan = nhanhieu.MaNhan,
                TenNhan = nhanhieu.TenNhan
            };

            // Thêm vào DbContext
            _context.Nhanhieus.Add(nhanhieunew);
            await _context.SaveChangesAsync();

            // Trả về kết quả đã tạo
            return nhanhieunew;
        }

        public async Task<int> DeleteNhanhieu(int id)
        {
            var nhanhieu = await _context.Nhanhieus.FindAsync(id);
            if (nhanhieu == null)
            {
                return 0;
            }

            _context.Nhanhieus.Remove(nhanhieu);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<Nhanhieu?> UpdateNhanhieu(int id, [FromBody] Nhanhieu updatedNhanhieu)
        {
            // Tìm danh mục theo ID
            var existingNhanhieu = await _context.Nhanhieus.FindAsync(id);

            if (existingNhanhieu == null)
            {
                return null; // Nếu không tồn tại, trả về null
            }

            // Chỉ cập nhật TenDanhmuc từ updatedDanhmuc
            existingNhanhieu.TenNhan = updatedNhanhieu.TenNhan;

            // Lưu thay đổi
            _context.Nhanhieus.Update(existingNhanhieu);
            await _context.SaveChangesAsync();

            return existingNhanhieu;
        }
    }
}
