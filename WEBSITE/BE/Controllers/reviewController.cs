using BE.Model;
using BE.Models;
using BE.Object;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewRepositoryADONET _review;
        private readonly db_websitebanhangContext _context;

        // Sử dụng dependency injection trong constructor
        public ReviewController()
        {
            _review = new ReviewRepositoryADONET();
         
        }

        // Lấy đánh giá của sản phẩm
        [HttpGet("GetReview")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview([FromQuery] string masanpham)
        {
            try
            {
                var listdanhgia = await _review.GETREVIEWS(masanpham);
                if (listdanhgia == null || !listdanhgia.Any())
                {
                    return NotFound(new { EC = 1, Message = "Không tìm thấy đánh giá cho sản phẩm này." });
                }
                return Ok(new { EC = 0, Data = listdanhgia });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { EC = 2, Message = "Lỗi server: " + ex.Message });
            }
        }

        // Tạo mới đánh giá
        [HttpPost("CreateReview")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] danhgia review1)
        {
            try
            {
                // Tạo mới đánh giá
                int kq = await _review.Createreview(review1);
                if (kq == 0)
                {
                    return BadRequest(new { EC = 1, Message = "Không thể tạo đánh giá cho sản phẩm này." });
                }

                // Lấy review từ cơ sở dữ liệu (hoặc từ repository) và trả về đối tượng review đã tạo
                var createdReview = new Review
                {
                    MaReview = review1.maReview,
                    NoiDung = review1.noiDung,
                    NgayNhap = review1.ngayNhap,
                    SoSao = review1.soSao,
                    Taikhoan = review1.taikhoan,
                    MaSanpham = review1.maSanpham
                };

                return CreatedAtAction(nameof(GetReview), new { masanpham = createdReview.MaSanpham }, createdReview);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { EC = 2, Message = "Lỗi server: " + ex.Message });
            }
        }
    }
}
