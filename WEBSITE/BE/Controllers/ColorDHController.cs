using BE.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorDHController : Controller
    {
        private readonly IColor _colorDH;

        public ColorDHController(IColor colorDH)
        {
            _colorDH = colorDH;
        }

        // API: Lấy danh sách tất cả colors
        [HttpGet(Name = "GetAllColors")]
        public async Task<IActionResult> GetAllColors()
        {
            try
            {
                var colors = await _colorDH.GetAllColors();

                if (colors == null || !colors.Any())
                {
                    return NotFound("Không tìm thấy màu sắc nào.");
                }

                return Ok(colors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi hệ thống: {ex.Message}");
            }
        }
    }
}
