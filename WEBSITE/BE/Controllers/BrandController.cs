using BE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly IBrand _brand;

        public BrandController(IBrand brand)
        {
            _brand = brand;
        }

        // API: Lấy danh sách tất cả colors
        [HttpGet(Name = "GetAllBrands")]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var bands = await _brand.GetAllBrands();

                if (bands == null || !bands.Any())
                {
                    return NotFound("Không tìm thấy màu sắc nào.");
                }

                return Ok(bands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }
    }
}
