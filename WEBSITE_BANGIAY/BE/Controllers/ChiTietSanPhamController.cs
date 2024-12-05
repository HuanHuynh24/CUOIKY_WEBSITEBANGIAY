using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietSanPhamController : ControllerBase
    {
        private readonly webbangiayContext _context;

        public ChiTietSanPhamController(webbangiayContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chitietsanpham>>> GetChiTietSanPhams()
        {
            var chitiesp = await _context.Chitietsanphams
                .Include(h => h.MaSanphamNavigation) // Join với bảng Users
                .Include(h => h.MaSizeNavigation)
                .Include(h => h.MaMauNavigation)// Join với bảng Trangthai
                .Select(h => new
                {
                    MaChiTietSP = h.IdChitietSp,
                    TenSP = h.MaSanphamNavigation.TenSanpham,
                    GiaSP = h.Gia,
                    SoLuong = h.SoLuongTon,
                    HinhAnhSP = h.HinhAnh,
                    SizeSP = h.MaSizeNavigation.MaSize,
                    MauSP = h.MaMauNavigation.TenMau
                })
                .ToListAsync();

            return Ok(chitiesp);
        }
    }
}
