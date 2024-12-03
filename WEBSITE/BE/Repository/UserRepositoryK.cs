using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Models
{
    public class UserRepositoryK:IUserRepositoryK
    {
        private readonly db_websitebanhangContext appDbContext;
        public UserRepositoryK(db_websitebanhangContext appDbContext) {
            this.appDbContext = appDbContext;
        }

        public async Task<User> GetByUsername(string Taikhoan)
        {
            return await appDbContext.Users.FirstOrDefaultAsync(us => us.Taikhoan.Trim().Equals(Taikhoan));
        }

		public async Task<User> Login(string Taikhoan, string Matkhau)
		{
			return await appDbContext.Users.FirstOrDefaultAsync(us =>
		 us.Taikhoan.Trim().Equals(Taikhoan) && us.Matkhau.Equals(Matkhau));
		}
	}
}
