using BE.Interface;
using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class ColorDH : IColor
    {
        private readonly db_websitebanhangContext db_connect;

        public ColorDH(db_websitebanhangContext db_connect)
        {
            this.db_connect = db_connect;
        }

        public async Task<IEnumerable<Color>> GetAllColors()
        {
            return await db_connect.Colors.ToListAsync();
        }
    }
}
