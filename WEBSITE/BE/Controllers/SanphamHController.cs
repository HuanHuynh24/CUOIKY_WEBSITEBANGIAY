using BE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanphamHController : Controller
    {
        private readonly SanphamH sp;

        public SanphamHController(SanphamH sp)
        {
            this.sp = sp;
        }

        [HttpGet(Name = "sanpham")]
        public async Task<IActionResult> GetAllSanphams()
        {
            try
            {
                var sanphams = await sp.getAllSanphams(); 
                return Ok(sanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xử lý yêu cầu.", error = ex.Message });
            }
        }
    }
}
