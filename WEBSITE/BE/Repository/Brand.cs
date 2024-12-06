using BE.Interface;
using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class Brand : IBrand
    {
        private readonly db_websitebanhangContext db_connect;

        public Brand(db_websitebanhangContext db_connect)
        {
            this.db_connect = db_connect;
        }

        public async Task<IEnumerable<Nhanhieu>> GetAllBrands()
        {
            return await db_connect.Nhanhieus.ToListAsync();
        }
    }
}
