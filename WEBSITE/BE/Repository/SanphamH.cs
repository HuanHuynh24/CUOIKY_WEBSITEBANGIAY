using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class SanphamH
    {
        private readonly db_websitebanhangContext _context;

        public SanphamH(db_websitebanhangContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Object>> getAllSanphams()
        {

            var sanphams = await _context.Sanphams
                .Include(sp => sp.MaNhanNavigation)
                .Include(sp => sp.MaKhuyenmaiNavigation)
                .Include(sp => sp.Chitietsanphams)
                .ThenInclude(ct => ct.MaMauNavigation)
                .Select(sp =>  new
                {
                    sp.TenSanpham,
                    sp.MoTa,
                    Chitietsanpham = sp.Chitietsanphams.Select(ct => new
                    {
                        ct.HinhAnh,
                        ct.SoLuongTon,
                        ct.Gia,
                        ct.MaMauNavigation.TenMau
                    }),
                    Nhanhieu = sp.MaNhanNavigation.TenNhan,
                    phantramkhuyenmai = sp.MaKhuyenmaiNavigation.PhanTram,
                }).ToListAsync();

            return sanphams;
        } 
    }
}
