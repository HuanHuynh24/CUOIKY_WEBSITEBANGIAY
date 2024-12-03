using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Models
{
    public interface IUserRepositoryK
    {
        Task<User> GetByUsername(string Taikhoan);
		
	}
}
