using BE.Models;
using BE.Object;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Model
{
    public class ReviewRepositoryADONET
    {
        private readonly db_websitebanhangContext _context;

        // Constructor nhận db_websitebanhangContext từ Dependency Injection
        public ReviewRepositoryADONET()
        {
            _context =new db_websitebanhangContext();
        }

        // Lấy danh sách review theo mã sản phẩm
        public async Task<IEnumerable<Review>> GETREVIEWS(string masanpham)
        {
            try
            {
                var listReview = await _context.Reviews
                    .Where(r => r.MaSanpham == masanpham)
                    .ToListAsync();
                return listReview;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return null;
            }
        }

        // Tạo mới một review
        public async Task<int> Createreview(danhgia review)
        {
            try
            {
                var rv = new Review
                {
                    MaReview = review.maReview,
                    NoiDung = review.noiDung,
                    NgayNhap = review.ngayNhap,
                    SoSao = review.soSao,
                    Taikhoan = review.taikhoan,
                    MaSanpham = review.maSanpham
                };

                _context.Reviews.Add(rv);

                // Lưu thay đổi vào cơ sở dữ liệu
                await _context.SaveChangesAsync();

                return 1; // Thành công
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return 0; // Thất bại
            }
        }
    }
}
