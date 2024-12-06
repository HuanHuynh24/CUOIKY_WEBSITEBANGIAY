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

        // Lấy thống kê doanh thu theo loại, năm và tháng
        [HttpGet("thongke")]
        public async Task<ActionResult<IEnumerable<Thongke>>> thongke([FromQuery] string type, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                // Gọi phương thức để lấy thống kê doanh thu
                var list = await _thongkeRepository.thongkedoanhthu(type, year, month);

                // Kiểm tra nếu không có dữ liệu
                if (list == null || !list.Any())
                {
                    return Ok(new { EC = 1, Message = "Không tìm thấy thống kê doanh thu cho loại sản phẩm này." });
                }

                return Ok(new { EC = 0, Data = list });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi server
                return Ok(new { EC = 2, Message = "Lỗi server: " + ex.Message });
            }
        }
    }
}
