using BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersControllerK : ControllerBase
    {
        IUserRepositoryK userRepository;
        public UsersControllerK(IUserRepositoryK userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> GetUser(string Taikhoan)
        {
            try
            {
                if (Taikhoan == "")
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "EROR");
                }
                else
                {
                    return Ok(await userRepository.GetByUsername(Taikhoan));
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,ex.Message);
            }
        }

	}
}
