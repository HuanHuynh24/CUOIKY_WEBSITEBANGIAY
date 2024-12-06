using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrangThaiController : ControllerBase
    {
        private readonly TrangThaiRepository _repository;

        public TrangThaiController(TrangThaiRepository repository)
        {
            _repository = repository;
        }

        // GET: api/DanhMuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trangthai>>> GetTrangthais()
        {
            try
            {

                return Ok(await _repository.GetTrangthais());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
        // GET: api/Danhmuc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trangthai>> GetTrangthai(int id)
        {
            try
            {
                var trangthai = await _repository.GetTrangthai(id);

                if (trangthai == null)
                {
                    return NotFound();
                }

                return trangthai;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
    }
}
