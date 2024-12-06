using BE.Interface;
using BE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChitietsanphamDHController : Controller
    {
        private readonly IChitietsanphamDH _repository;

        // Inject repository vào controller
        public ChitietsanphamDHController(IChitietsanphamDH repository)
        {
            _repository = repository;
        }

        // Endpoint: Lấy tất cả chi tiết sản phẩm
        [HttpGet(Name = "GetAllChitietsanphams")]
        public async Task<IActionResult> GetAllChitietsanphams()
        {
            try
            {
                var chitietsanphams = await _repository.GetAllChitietsanphams();
                return Ok(chitietsanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xử lý yêu cầu.", error = ex.Message });
            }
        }

        // Endpoint: Lấy chi tiết sản phẩm theo khoảng giá
        [HttpGet("by-price-range")]
        public async Task<IActionResult> GetChitietsanphamsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            try
            {
                var chitietsanphams = await _repository.GetChitietsanphamsByPriceRange(minPrice, maxPrice);

                if (!chitietsanphams.Any())
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm trong khoảng giá này." });
                }

                return Ok(chitietsanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xử lý yêu cầu.", error = ex.Message });
            }
        }

        [HttpGet("filter-by-color/{maMau}")]
        public async Task<IActionResult> GetByColor(int maMau)
        {
            try
            {
                var data = await _repository.GetByColor(maMau);
                if (data == null || !data.Any())
                    return NotFound(new { message = "Không tìm thấy sản phẩm với màu được chỉ định." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xử lý yêu cầu.", error = ex.Message });
            }
        }

        [HttpGet("filter-by-nhanhieu/{maNhan}")]
        public async Task<IActionResult> GetChitietsanphamByNhanhieu(int maNhan)
        {
            try
            {
                var chitietsanphams = await _repository.GetChitietsanphamByNhanhieu(maNhan);

                if (!chitietsanphams.Any())
                    return NotFound(new { message = "Không tìm thấy chi tiết sản phẩm với nhãn hiệu này." });

                return Ok(chitietsanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi.", error = ex.Message });
            }
        }
        // API: Lấy chi tiết sản phẩm theo danh mục
        [HttpGet("byDanhmuc/{maDanhMuc}")]
        public async Task<IActionResult> GetByDanhmuc(int maDanhMuc)
        {
            try
            {
                var result = await _repository.GetChitietsanphamByDanhmuc(maDanhMuc);
                if (result == null || !result.Any())
                {
                    return NotFound("Không tìm thấy chi tiết sản phẩm cho danh mục này.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }
    }
}
