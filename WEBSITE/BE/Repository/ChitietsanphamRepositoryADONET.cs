using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using BE.Object;
using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Model
{
    public class ChitietsanphamRepositoryADONET
    {
        private readonly db_websitebanhangContext _context;

        public ChitietsanphamRepositoryADONET()
        {
            _context = new db_websitebanhangContext();
        }

        // Lấy danh sách sản phẩm theo mã sản phẩm
        public async Task<IEnumerable<Sanphamsearch>> GETSANPHAMS(string key)
        {
            try

            {
                var listSanPham = await (from sanpham in _context.Sanphams
                                         join chitietSanpham in _context.Chitietsanphams
                                             on sanpham.MaSanpham equals chitietSanpham.MaSanpham
                                         where sanpham.TenSanpham.StartsWith(key)
                                         group chitietSanpham by new { sanpham.MaSanpham, sanpham.TenSanpham } into g
                                         select new Sanphamsearch
                                         {
                                             masanpham = g.Key.MaSanpham,  // MaSanpham từ nhóm
                                             tensanpham = g.Key.TenSanpham,  // TenSanpham từ nhóm
                                             hinhanh = g.FirstOrDefault().HinhAnh,  // Lấy HinhAnh của chi tiết sản phẩm đầu tiên
                                             Gia = g.FirstOrDefault().Gia  // Lấy Giá của chi tiết sản phẩm đầu tiên
                                         }).ToListAsync();
                return listSanPham;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }

        // Lấy danh sách màu
        public async Task<IEnumerable<Color>> GETCOLORS()
        {
            try
            {
                var listColor = await _context.Colors.ToListAsync();
                return listColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }

        // Lấy danh sách kích thước
        public async Task<IEnumerable<Size>> GETSIZES()
        {
            try
            {
                var listSize = await _context.Sizes.ToListAsync();
                return listSize;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }

        // Lấy chi tiết sản phẩm theo mã sản phẩm, mã màu, và mã size
        public async Task<ChitietSanphamWithReviews> GETCHIIETSANPHAM(string masanpham, int mamau, int masize)
        {
            try
            {

                string Lmasp = masanpham.Length >= 6
                ? masanpham.Substring(masanpham.Length - 6, 6)
                 : masanpham.PadLeft(6, '0');
                string Lmamau = mamau >= 0
                    ? "0" + mamau.ToString("D1")                 
                    : "01";
                string Lsize = masize.ToString();

                string idchitietSP =Lmasp+Lmamau+Lsize;
                Console.WriteLine(idchitietSP);
                // Fetch product details (single object)
                var chitietSanphaml = await (from chitietSanpham in _context.Chitietsanphams
                                            join sanpham in _context.Sanphams
                                                on chitietSanpham.MaSanpham equals sanpham.MaSanpham
                                            join color in _context.Colors
                                                on chitietSanpham.MaMau equals color.MaMau
                                            join size in _context.Sizes
                                                on chitietSanpham.MaSize equals size.MaSize
                                            where chitietSanpham.IdChitietSp == idchitietSP
                                             select new
                                             {
                                                 ChitietSanpham = new Chitietsanpham
                                                 {
                                                     IdChitietSp = chitietSanpham.IdChitietSp,
                                                     HinhAnh = chitietSanpham.HinhAnh,
                                                     SoLuongTon = chitietSanpham.SoLuongTon,
                                                     Gia = chitietSanpham.Gia,
                                                     MaMau = chitietSanpham.MaMau,
                                                     MaSanpham = chitietSanpham.MaSanpham,
                                                     MaSize = chitietSanpham.MaSize,
                                                     MaMauNavigation = color,
                                                     MaSanphamNavigation = sanpham,
                                                     MaSizeNavigation = size,
                                                     Chitiethoadons = chitietSanpham.Chitiethoadons
                                                 }
                                             }).FirstOrDefaultAsync();


                if (chitietSanphaml == null)
                    return null;

                // Fetch reviews (multiple objects)
                var danhgiaSanpham = await (from danhgia1 in _context.Reviews
                                            join user in _context.Users
                                                on danhgia1.Taikhoan equals user.Taikhoan
                                            where danhgia1.MaSanpham == masanpham
                                            select new ReviewDTO
                                            {
                                                NoiDung = danhgia1.NoiDung,
                                                sosao=danhgia1.SoSao,
                                                NgayNhap = danhgia1.NgayNhap,
                                                Ten = user.Ten
                                            }).ToListAsync();

                // Combine the product and the reviews into one object
                ChitietSanphamWithReviews a = new ChitietSanphamWithReviews
                {
                
                    ChitietSanpham = chitietSanphaml.ChitietSanpham,
                    Reviews = danhgiaSanpham
                };
                return  a;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }
    }
}
