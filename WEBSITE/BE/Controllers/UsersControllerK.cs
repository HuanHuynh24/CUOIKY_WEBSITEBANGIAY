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
		//[HttpPost("Login")]
		//public async Task<IActionResult> Login([FromBody] User loginUser)
		//{
		//	try
		//	{
		//		if (string.IsNullOrEmpty(loginUser.Taikhoan) || string.IsNullOrEmpty(loginUser.Matkhau))
		//		{
		//			return BadRequest("Tài khoản và mật khẩu không được để trống.");
		//		}

		//		var user = await userRepository.Login(loginUser.Taikhoan, loginUser.Matkhau);

		//		if (user == null)
		//		{
		//			return Unauthorized("Tài khoản hoặc mật khẩu không đúng.");
		//		}
		//		return Ok(new
		//		{
		//			Message = "Đăng nhập thành công",
		//			User = new
		//			{
		//				user.Taikhoan,
		//				user.Ten,
		//				user.Email,
		//				user.sdt,
		//				user.Diachi
		//			}
		//		});
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(StatusCodes.Status500InternalServerError, $"Lỗi: {ex.Message}");
		//	}
		//}
	}
}
