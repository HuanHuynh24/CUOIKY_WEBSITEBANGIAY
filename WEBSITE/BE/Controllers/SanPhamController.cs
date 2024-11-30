using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SanPhamController : Controller
    {
        private readonly db_websitebanhangContext _context;
        public SanPhamController(db_websitebanhangContext context) 
        {
            _context = context;
        }
        [HttpGet(Name = "sanpham")]
        public async Task <IEnumerable<Sanpham>> Getsanpham()
        {
            return await _context.Sanphams.ToListAsync();
        }

    }
}
