using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly SizeRepository _repository;

        public SizeController(SizeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/DanhMuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Size>>> GetSizes()
        {
            try
            {

                return Ok(await _repository.GetSizes());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
        // GET: api/Danhmuc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Size>> GetSize(int id)
        {
            try
            {
                var size = await _repository.GetSize(id);

                if (size == null)
                {
                    return NotFound();
                }

                return size;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
    }
}
