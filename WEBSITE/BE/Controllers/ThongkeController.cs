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
    public class ThongkeController : ControllerBase
    {
        private readonly ThongkedoanhthuRepositoryADONET _thongkeRepository;

        // Constructor sử dụng Dependency Injection
        public ThongkeController(ThongkedoanhthuRepositoryADONET thongkeRepository)
        {
            _thongkeRepository = thongkeRepository;
        }

        // API lấy top 10 sản phẩm bán chạy nhất
        [HttpGet("getbestsanpham")]
        public async Task<ActionResult<IEnumerable<Thongke>>> GetTop10BestSellingProducts()
        {
            try
            {
                var list = await _thongkeRepository.GetTop10BestSellingProductsAsync();
                if (list == null || !list.Any())
                {
                    return NotFound(new { EC = 1, Message = "Không tìm thấy sản phẩm bán chạy." });
                }

                return Ok(new { EC = 0, Data = list });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { EC = 2, Message = "Lỗi server: " + ex.Message });
            }
        }

        // API lấy thống kê doanh thu theo loại, năm và tháng
        [HttpGet("thongke")]
        public async Task<ActionResult<IEnumerable<Thongke>>> GetRevenueStatistics([FromQuery] string type, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                // Gọi phương thức để lấy thống kê doanh thu
                var list = await _thongkeRepository.thongkedoanhthu(type, year, month);

                // Kiểm tra nếu không có dữ liệu
                if (list == null || !list.Any())
                {
                    return NotFound(new { EC = 1, Message = "Không tìm thấy thống kê doanh thu cho loại sản phẩm này." });
                }

                return Ok(new { EC = 0, Data = list });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi server
                return StatusCode(500, new { EC = 2, Message = "Lỗi server: " + ex.Message });
            }
        }
    }
}
