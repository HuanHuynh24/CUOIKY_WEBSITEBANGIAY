using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ColorRepository _repository;

        public ColorController(ColorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/DanhMuc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            try
            {

                return Ok(await _repository.GetColors());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
        // GET: api/Danhmuc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Color>> GetColor(int id)
        {
            try
            {
                var color = await _repository.GetColor(id);

                if (color == null)
                {
                    return NotFound();
                }

                return color;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "ERROR");
            }
        }
    }
}
