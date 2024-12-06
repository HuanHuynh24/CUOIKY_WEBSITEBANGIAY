using BE.Interface;
using BE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChitiethoadonHController : Controller
    {
        private readonly IChitiethoadonH _controller;

        public ChitiethoadonHController(IChitiethoadonH controller)
        {
            _controller = controller;
        }

        [HttpPost("users/{idUser}/products/{idProduct}")]
        public async Task<IActionResult> GetProductByUser(string idUser, string idProduct)
        {
            try
            {
                bool ktr =await _controller.kTracthd(idUser, idProduct);
                if(ktr)
                {
                    return StatusCode(201, new { message = "Sản phẩm đã tồn tại trong giỏ hàng" });
                }
                // Xử lý logic
                var result = await _controller.addProduct(idUser, idProduct);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy sản phẩm." });
                }
                return Ok(new { message = "Thêm vào giỏ hàng thành công" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server: " + ex.Message });
            }
        }
        [HttpGet("giohang/{idUser}")]
        public async Task<IActionResult> getGiohang(String idUser)
        {
            try
            {
                var sanphams = await _controller.getGiohang(idUser);
                return Ok(sanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xử lý yêu cầu.", error = ex.Message });
            }
        }

    }
}
