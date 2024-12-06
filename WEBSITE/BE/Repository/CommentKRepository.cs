using BE.Models;
using Microsoft.EntityFrameworkCore;

namespace BE.Repository
{
    public class CommentKRepository
    {
        private readonly db_websitebanhangContext context1;

        public CommentKRepository(db_websitebanhangContext context1)
        {
            this.context1 = context1;
        }

        public async Task<Comment?> CreateComment(int idBlog, string taikhoan, string content)
        {
            var blog = await context1.Blogs.FirstOrDefaultAsync(b => b.MaBlog == idBlog.ToString());
            if (blog == null)
            {
                return null; 
            }
            var user = await context1.Users.FirstOrDefaultAsync(u => u.Taikhoan == taikhoan);
            if (user == null)
            {
                return null;
            }
            int maxCommentId = (await context1.Comments.MaxAsync(c => (int?)Convert.ToInt32(c.MaComment)) ?? 6) + 1;
            var newComment = new Comment
            {
                MaComment = maxCommentId.ToString(),
                NoiDung = content,
                NgayTao = DateTime.Now,
                MaBlog = blog.MaBlog,
                Taikhoan = user.Taikhoan
            };

            context1.Comments.Add(newComment);
            await context1.SaveChangesAsync();

            return newComment;
        }
    }
}
