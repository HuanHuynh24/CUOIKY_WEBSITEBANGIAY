using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly SanPhamRepository _repository;

        public SanPhamController(SanPhamRepository sanPhamRepository)
        {
            _repository = sanPhamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSanphams()
        {
            try
            {
                var sanphams = await _repository.GetSanphams();
                return Ok(sanphams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy danh sách sản phẩm", error = ex.Message });
            }
        }

        // GET: api/Sanpham/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSanpham(string id)
        {
            try
            {
                var sanpham = await _repository.GetSanpham(id);

                if (sanpham == null)
                {
                    return NotFound($"Sản phẩm với ID {id} không tồn tại.");
                }

                return Ok(sanpham);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi server: {ex.Message}");
            }
        }

        // POST: api/Sanpham
        [HttpPost]
        public async Task<IActionResult> AddSanpham(Sanpham sanpham)
        {
            try
            {

                var createdSanpham = await _repository.AddSanpham(sanpham);
                Console.WriteLine(createdSanpham);

                return CreatedAtAction(nameof(GetSanpham), new { id = createdSanpham.MaSanpham }, createdSanpham);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        // PUT: api/Sanpham/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSanpham(string id, Sanpham sanpham)
        {
            if (sanpham == null || id != sanpham.MaSanpham)
            {
                return BadRequest("Thông tin không hợp lệ.");
            }
            try
            {
                var updatedSanpham = await _repository.UpdateSanpham(id, sanpham);

                if (updatedSanpham == null)
                {
                    return NotFound($"Sản phẩm với ID {id} không tồn tại.");
                }

                return Ok(updatedSanpham);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi khi cập nhật sản phẩm: {ex.Message}");
            }
        }

        // DELETE: api/Sanpham/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanpham(string id)
        {
            try
            {
                var result = await _repository.DeleteSanpham(id);

                if (result == 0)
                {
                    return NotFound($"Sản phẩm với ID {id} không tồn tại.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
