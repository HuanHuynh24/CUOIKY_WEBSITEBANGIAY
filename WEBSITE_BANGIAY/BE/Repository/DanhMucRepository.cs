using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace BE.Repository
{
    public class DanhMucRepository
    {
        private readonly webbangiayContext _context;
        public DanhMucRepository(webbangiayContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Danhmuc>> GetDanhmucs()
        {
            return await _context.Danhmucs.ToListAsync();
        }
        public async Task<Danhmuc> GetDanhmuc(int id)
        {
            return await _context.Danhmucs.FirstOrDefaultAsync(e => e.MaDanhmuc == id);
        }
        public async Task<Danhmuc> AddDanhmuc(int madanhmuc, string tendanhmuc)
        {
            // Tạo một đối tượng Danhmuc mới từ tham số truyền vào
            var danhmuc = new Danhmuc
            {
                MaDanhmuc = madanhmuc,
                TenDanhmuc = tendanhmuc
            };

            // Thêm vào DbContext
            _context.Danhmucs.Add(danhmuc);
            await _context.SaveChangesAsync();

            // Trả về kết quả đã tạo
            return danhmuc;
        }
        public async Task<Danhmuc> AddDanhmucs([FromBody] Danhmuc danhmuc)
        {

            // Tạo một đối tượng Danhmuc mới từ tham số truyền vào
            var danhmucnew = new Danhmuc
            {
                MaDanhmuc = danhmuc.MaDanhmuc,
                TenDanhmuc = danhmuc.TenDanhmuc
            };

            // Thêm vào DbContext
            _context.Danhmucs.Add(danhmucnew);
            await _context.SaveChangesAsync();

            // Trả về kết quả đã tạo
            return danhmucnew;
        }

        public async Task<int> DeleteDanhmuc(int id) {
            var danhmuc = await _context.Danhmucs.FindAsync(id);
            if (danhmuc == null)
            {
                return 0;
            }

            _context.Danhmucs.Remove(danhmuc);
            await _context.SaveChangesAsync();

            return 1;
        }
        /*public async Task<Danhmuc> UpdateDanhmuc(int id, string tendanhmuc)
        {
            
            var danhmuc = await _context.Danhmucs.FindAsync(id);
            if (danhmuc == null)
            {
                return null;
            }

            danhmuc.TenDanhmuc = tendanhmuc;

            await _context.SaveChangesAsync();

            return danhmuc;
        }*/

        public async Task<Danhmuc?> UpdateDanhmuc(int id, Danhmuc updatedDanhmuc)
        {
            // Tìm danh mục theo ID
            var existingDanhmuc = await _context.Danhmucs.FindAsync(id);

            if (existingDanhmuc == null)
            {
                return null; // Nếu không tồn tại, trả về null
            }

            // Chỉ cập nhật TenDanhmuc từ updatedDanhmuc
            existingDanhmuc.TenDanhmuc = updatedDanhmuc.TenDanhmuc;

            // Lưu thay đổi
            _context.Danhmucs.Update(existingDanhmuc);
            await _context.SaveChangesAsync();

            return existingDanhmuc;
        }
    }
}
