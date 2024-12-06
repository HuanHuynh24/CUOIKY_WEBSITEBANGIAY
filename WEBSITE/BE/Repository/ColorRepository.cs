using BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class ColorRepository
    {
        private readonly db_websitebanhangContext _context;
        public ColorRepository(db_websitebanhangContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }
        public async Task<Color> GetColor(int id)
        {
            return await _context.Colors.FirstOrDefaultAsync(e => e.MaMau == id);
        }
    }
}
