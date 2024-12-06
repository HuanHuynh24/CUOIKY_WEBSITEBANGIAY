using BE.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE.Repository
{
    public class BlogKRepository
    {
        private readonly db_websitebanhangContext _context;

        public BlogKRepository(db_websitebanhangContext context)
        {
            _context = context;
        }
        public async Task <IEnumerable<Object>> GetAllBlog()
        {
            var danhmucs = await _context.Danhmucs.Include(dm => dm.Blogs)
                .ThenInclude(bl=>bl.Comments).Select(dm => new
                {
                     tenDanhMuc = dm.TenDanhmuc,
                     SoLuongBlog = dm.Blogs.Count(),
                    Blogs = dm.Blogs.Select(bl => new
                     {
                        bl.MaBlog,
                         bl.Tieude,
                        NoidungTrichDan = !string.IsNullOrEmpty(bl.Noidung) && bl.Noidung.IndexOf('!') >= 0
                    ? bl.Noidung.Substring(0, bl.Noidung.IndexOf('!'))
                    : bl.Noidung,
                        bl.Ngaytao,
                         bl.Hinhanh,
                         CountCmt = bl.Comments.Count()
                     })
                     
                }).ToListAsync();
            return danhmucs;
        }
        public async Task<IEnumerable<object>> GetBlogDetail(int id)
        {
            var blogDetail = await _context.Blogs
                .Include(b => b.Comments) 
                .ThenInclude(c => c.TaikhoanNavigation)
                .Where(b => b.MaBlog == id.ToString())
                .Select(b => new
                {
                    b.MaBlog,
                    b.Tieude,
                    b.Noidung,
                    b.Ngaytao,
                    b.Hinhanh,
                    CountCmt = b.Comments.Count(),
                    Danhmuc = b.MaDanhmucNavigation.TenDanhmuc,
                    Comments = b.Comments.Select(c => new
                    {
                        c.MaComment,
                        c.NoiDung,
                        c.NgayTao,
                        TenNguoiCmt = c.TaikhoanNavigation.Ten
                    })
                })
                .ToListAsync();

            return blogDetail;
        }

    }
}
