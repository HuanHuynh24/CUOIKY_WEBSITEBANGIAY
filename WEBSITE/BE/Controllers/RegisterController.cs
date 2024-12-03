using BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegisterController : Controller
    {
        private readonly db_websitebanhangContext _context;

        public RegisterController(db_websitebanhangContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ!", errors = ModelState });
            }

            // Kiểm tra tài khoản đã tồn tại
            var existingUser = await _context.Users.FindAsync(user.Taikhoan);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Tài khoản đã tồn tại!" });
            }

            // Chỉ thêm các thuộc tính chính
            var newUser = new User
            {
                Taikhoan = user.Taikhoan,
                Matkhau = user.Matkhau, // Không mã hóa
                Ten = user.Ten,
                Sdt = user.Sdt,
                Email = user.Email,
                DiaChi = user.DiaChi
            };

            // Thêm tài khoản vào database
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            // Trả về thông tin khi thành công
            return Ok(new
            {
                message = "Đăng ký thành công!",
                user = new
                {
                    Taikhoan = newUser.Taikhoan,
                    Matkhau = newUser.Matkhau,
                    Ten = newUser.Ten,
                    Sdt = newUser.Sdt,
                    Email = newUser.Email
                }
            });
        }
    }
}
