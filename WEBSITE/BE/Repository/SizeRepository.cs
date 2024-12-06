using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class SizeRepository
    {
        private readonly db_websitebanhangContext _context;
        public SizeRepository(db_websitebanhangContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await _context.Sizes.ToListAsync();
        }
        public async Task<Size> GetSize(int id)
        {
            return await _context.Sizes.FirstOrDefaultAsync(e => e.MaSize == id);
        }
    }
}
