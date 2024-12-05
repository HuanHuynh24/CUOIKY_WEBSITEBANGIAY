using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuController : ControllerBase
    {
        private readonly ThuongHieuRepository _repository;

        public ThuongHieuController(ThuongHieuRepository repository)
        {
            _repository = repository;
        }

        // GET: api/ThuongHieu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nhanhieu>>> GetNhanhieus()
        {
            try
            {

                return Ok(await _repository.GetNhanhieus());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        // GET: api/ThuongHieu/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Nhanhieu>> GetNhanhieu(int id)
        {
            try
            {
                var nhanhieu = await _repository.GetNhanhieu(id);

                if (nhanhieu == null)
                {
                    return NotFound();
                }

                return nhanhieu;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Nhanhieu>> AddNhanhieu(Nhanhieu nhanhieu)
        {
            try
            {

                var nhanhieunew = await _repository.AddNhanhieu(nhanhieu);

                return CreatedAtAction(nameof(GetNhanhieu), new { id = nhanhieunew.MaNhan }, nhanhieunew);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhanhieu(int id, Nhanhieu nhanhieu)
        {
            if (nhanhieu == null || id != nhanhieu.MaNhan)
            {
                return BadRequest("Thông tin không hợp lệ.");
            }

            try
            {
                // Gọi Repository để cập nhật
                var updatedNhanhieu = await _repository.UpdateNhanhieu(id, nhanhieu);

                if (updatedNhanhieu == null)
                {
                    return NotFound($"Thương hiệu với ID {id} không tồn tại.");
                }

                return Ok(updatedNhanhieu);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật thương hiệu.");
            }
        }

        // DELETE: api/ThuongHieu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhanhieu(int id)
        {
            try
            {
                var result = await _repository.DeleteNhanhieu(id);
                if (result == 0)
                {
                    return NotFound($"Thương hiệu với ID {id} không tồn tại.");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa thương hiệu.");
            }
        }
    }
}
