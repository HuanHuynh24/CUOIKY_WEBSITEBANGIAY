using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BE.Repository
{
    public class SanPhamRepository
    {
        private readonly db_websitebanhangContext _context;
        public SanPhamRepository(db_websitebanhangContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SanphamDTO>> GetSanphams()
        {
            var sanphams = await _context.Sanphams
                .Include(h => h.MaNhanNavigation) // Join với bảng MaNhan
                .Include(h => h.MaDanhmucNavigation) // Join với bảng DanhMuc
                .Include(h => h.MaKhuyenmaiNavigation) // Join với bảng KhuyenMai
                .Select(h => new SanphamDTO
                {
                    MaSP = h.MaSanpham,
                    TenSP = h.TenSanpham,
                    MotaSP = h.MoTa,
                    LuotYeuThichSP = h.SoLuongyeuthich,
                    ThuongHieu = h.MaNhanNavigation.TenNhan,
                    DanhMucSP = h.MaDanhmucNavigation.TenDanhmuc,
                    KhyenMaiSP = h.MaKhuyenmaiNavigation.PhanTram
                    //UpdateStatus = "Update" // Static button text
                })
                .ToListAsync();

            return sanphams;
        }
        public async Task<SanphamDTO?> GetSanpham(string id)
        {
            var sanpham = await _context.Sanphams
                        .Include(h => h.MaNhanNavigation)
                        .Include(h => h.MaDanhmucNavigation)
                        .Include(h => h.MaKhuyenmaiNavigation)
                        .Where(h => h.MaSanpham == id)
                        .Select(h => new SanphamDTO
                        {
                            MaSP = h.MaSanpham,
                            TenSP = h.TenSanpham,
                            MotaSP = h.MoTa,
                            LuotYeuThichSP = h.SoLuongyeuthich,
                            ThuongHieu = h.MaNhanNavigation.TenNhan,
                            DanhMucSP = h.MaDanhmucNavigation.TenDanhmuc,
                            KhyenMaiSP = h.MaKhuyenmaiNavigation.PhanTram
                        })
                        .FirstOrDefaultAsync();

            return sanpham;
        }
        public async Task<Sanpham> AddSanpham([FromBody] Sanpham sanpham)
        {
            try
            {
                var sanphamnew = new Sanpham
                {
                    MaSanpham = sanpham.MaSanpham,
                    TenSanpham = sanpham.TenSanpham,
                    MoTa = sanpham.MoTa,
                    SoLuongyeuthich = sanpham.SoLuongyeuthich,
                    MaNhan = sanpham.MaNhan,
                    MaDanhmuc = sanpham.MaDanhmuc,
                    MaKhuyenmai = sanpham.MaKhuyenmai

                };

                // Thêm vào DbContext
                _context.Sanphams.Add(sanphamnew);
                await _context.SaveChangesAsync();
                return sanphamnew;
            }
            catch(Exception e)
            {
                return null;
                
            }
            

            // Trả về kết quả đã tạo
            
        }

        public async Task<Sanpham?> UpdateSanpham(string id, [FromBody] Sanpham updatedSanpham)
        {
            var sanpham = await _context.Sanphams.FirstOrDefaultAsync(h => h.MaSanpham == id);

            if (sanpham == null)
            {
                return null; // Sản phẩm không tồn tại
            }
            // Chỉ cập nhật
            sanpham.TenSanpham = updatedSanpham.TenSanpham;
            sanpham.MoTa = updatedSanpham.MoTa;
            sanpham.SoLuongyeuthich = updatedSanpham.SoLuongyeuthich;
            sanpham.MaNhan = updatedSanpham.MaNhan;
            sanpham.MaDanhmuc = updatedSanpham.MaDanhmuc;
            sanpham.MaKhuyenmai = updatedSanpham.MaKhuyenmai; // Cập nhật mã khuyến mãi
            await _context.SaveChangesAsync();

            return sanpham;
        }

        public async Task<int> DeleteSanpham(string id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return 0;
            }

            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
