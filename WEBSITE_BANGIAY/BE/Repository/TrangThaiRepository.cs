using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class TrangThaiRepository
    {
        private readonly webbangiayContext _context;
        public TrangThaiRepository(webbangiayContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Trangthai>> GetTrangthais()
        {
            return await _context.Trangthais.ToListAsync();
        }
        public async Task<Trangthai> GetTrangthai(int id)
        {
            return await _context.Trangthais.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
