using BE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentKController : ControllerBase
    {
        private readonly CommentKRepository comment;

        public CommentKController(CommentKRepository comment)
        {
            this.comment = comment;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment(int idBlog, string taikhoan, string content)
        {
            var newComment = await comment.CreateComment(idBlog, taikhoan, content);

            if (newComment == null)
            {
                return NotFound(new { Message = "Blog or User not found" });
            }

            return Ok(newComment);
        }
    }
}
