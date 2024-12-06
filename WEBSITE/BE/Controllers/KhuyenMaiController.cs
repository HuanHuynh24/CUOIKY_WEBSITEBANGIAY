using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly KhuyenMaiRepository _repository;
        public KhuyenMaiController(KhuyenMaiRepository repository)
        {
            _repository = repository;
        }
        // GET: api/KhuyenMai
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Khuyenmai>>> GetKhuyenmais()
        {
            try
            {

                return Ok(await _repository.GetKhuyenmais());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        // GET: api/KhuyenMai/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Khuyenmai>> GetKhuyenmai(string id)
        {
            try
            {
                var khuyenmai = await _repository.GetKhuyenmai(id);

                if (khuyenmai == null)
                {
                    return NotFound();
                }

                return khuyenmai;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Khuyenmai>> AddKhuyenmai(Khuyenmai khuyenmai)
        {
            try
            {

                var khuyenmainew = await _repository.AddKhuyenmai(khuyenmai);

                return CreatedAtAction(nameof(GetKhuyenmai), new { id = khuyenmainew.MaKhuyenmai }, khuyenmainew);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKhuyenmai(string id, [FromBody] Khuyenmai khuyenmai)
        {
            if (khuyenmai == null || id != khuyenmai.MaKhuyenmai)
            {
                return BadRequest("Thông tin không hợp lệ.");
            }

            try
            {
                // Gọi Repository để cập nhật
                var updatedKhuyenmai = await _repository.UpdateKhuyenmai(id, khuyenmai);

                if (updatedKhuyenmai == null)
                {
                    return NotFound($"Khuyến mãi với ID {id} không tồn tại.");
                }

                return Ok(updatedKhuyenmai);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật khuyến mãi.");
            }
        }

        // DELETE: api/KhuyenMai/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuyenmai(string id)
        {
            try
            {
                var result = await _repository.DeleteKhuyenmai(id);
                if (result == 0)
                {
                    return NotFound($"Khuyến mãi với ID {id} không tồn tại.");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa khuyến mãi.");
            }
        }
    }
}
