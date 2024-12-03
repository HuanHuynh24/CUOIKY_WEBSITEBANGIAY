using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly DanhMucRepository _repository;
       
        public DanhMucController(DanhMucRepository repository)
        {
            _repository = repository;
        }
        // GET: api/DanhMuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Danhmuc>>> GetDanhmucs()
        {
            try
            {

                return Ok(await _repository.GetDanhmucs());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
        // GET: api/Danhmuc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Danhmuc>> GetDanhmuc(int id)
        {
            try
            {
                var danhmuc = await _repository.GetDanhmuc(id);

                if (danhmuc == null)
                {
                    return NotFound();
                }

                return danhmuc;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }

        // POST: api/Danhmuc
        [HttpPost]
        public async Task<ActionResult<Danhmuc>> AddDanhmuc(int madanhmuc, string tendanhmuc)
        {
            try
            {

                var danhmucnew = await _repository.AddDanhmuc(madanhmuc, tendanhmuc);

                return CreatedAtAction(nameof(GetDanhmuc), new { id = danhmucnew.MaDanhmuc }, danhmucnew);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }

        }


        // PUT: api/Danhmuc/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDanhmuc(int id, string tendanhmuc)
        {

            try
            {
                var danhmuc = await _repository.UpdateDanhmuc(id, tendanhmuc);
                if (danhmuc == null)
                {
                    return NotFound($"Danh mục với ID {id} không tồn tại.");
                }

                return Ok(danhmuc);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi cập nhật danh mục.");
            }
        }

        // DELETE: api/Danhmuc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhmuc(int id)
        {
            try
            {
                var result = await _repository.DeleteDanhmuc(id);
                if (result == 0)
                {
                    return NotFound($"Danh mục với ID {id} không tồn tại.");
                }

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Lỗi khi xóa danh mục.");
            }
        }

    }
}
