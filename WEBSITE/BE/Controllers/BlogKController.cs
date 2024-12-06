using BE.Models;
using BE.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogKController : ControllerBase
    {
        private readonly BlogKRepository blogKRepository;

        public BlogKController(BlogKRepository blogKRepository)
        {
           this.blogKRepository = blogKRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getAllBlogs()
        {
            try
            {
                var blogs = await blogKRepository.GetAllBlog();
                return Ok(blogs);
            }catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi khi xử lý yêu cầu.",eror = ex.Message});
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getBlogDetail(int id)
        {
            try
            {
                var blogs = await blogKRepository.GetBlogDetail(id);
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã có lỗi khi xử lý yêu cầu.", eror = ex.Message });
            }
        }

    }
}
