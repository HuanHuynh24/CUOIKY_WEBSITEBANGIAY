using FE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FE.Controllers
{
	public class BlogController : Controller
	{
        private readonly HttpClient _httpClient;

        public BlogController()
        {
            _httpClient = new HttpClient();

        }
        public async Task<ActionResult> Blog()
        {
            var user = Session["UserSession"] as User;
            if (user == null)
            {
                TempData["EROR"] = "Bạn phải đăng nhập để sử dụng tính năng này.";
                return RedirectToAction("Login", "Account");
            }

            string apiUrl = "http://localhost:5288/api/BlogK";
            //string apiUrl = "http://localhost:5288/api/BlogK/GetBlogDetails"; 

            try
            {

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    List<DanhmucK> blogList = JsonConvert.DeserializeObject<List<DanhmucK>>(data);

                    return View(blogList);
                }
                else
                {
                    TempData["EROR"] = "Không thể lấy dữ liệu từ API.";
                    return View(new List<BlogK>());
                }
            }
            catch (Exception ex)
            {
                TempData["EROR"] = $"Đã xảy ra lỗi: {ex.Message}";
                return View(new List<BlogK>());
            }
        }

        public async Task<ActionResult> BlogDetail(int id)
        {
            var user = Session["UserSession"] as User;
            if (user == null)
            {
                TempData["EROR"] = "Bạn phải đăng nhập để sử dụng tính năng này.";
                return RedirectToAction("Login", "Account");
            }
            try
            {

                string apiUrl = $"http://localhost:5288/api/BlogK/{id}";

                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    TempData["EROR"] = "Không thể lấy dữ liệu từ API.";
                    return View(); 
                }

                var responseData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseData))
                {
                    TempData["EROR"] = "Không tìm thấy bài viết.";
                    return View(new List<BlogK>());
                }

                var blogDetail = JsonConvert.DeserializeObject<List<BlogK>>(responseData);

                return View(blogDetail);
            }
            catch (Exception ex)
            {
    
                TempData["EROR"] = $"Đã xảy ra lỗi: {ex.Message}";
                return View(new List<BlogK>()); 
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateCommentInBlog(int idBlog, string content)
        {
            var user = Session["UserSession"] as User;
            if (user == null)
            {
                TempData["EROR"] = "Bạn phải đăng nhập để sử dụng tính năng này.";
                return RedirectToAction("Login", "Account");
            }

            try
            {
                
                //string apiUrl = "http://localhost:5288/api/CommentK/create";

                var payload = new
                {
                    idBlog = idBlog,
                    taikhoan = user.Taikhoan, // Lấy từ Session
                    content = content
                };

                string apiUrl = $"http://localhost:5288/api/CommentK/create?idBlog={idBlog}&taikhoan={user.Taikhoan}&content={content}";

                var jsonPayload = JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(apiUrl, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SUCCESS"] = "Thêm bình luận thành công.";
                    return RedirectToAction("BlogDetail", new { id = idBlog });
                }
                else
                {
                    TempData["EROR"] = "Không thể thêm bình luận.";
                    return RedirectToAction("BlogDetail", new { id = idBlog });
                }
            }
            catch (Exception ex)
            {
                TempData["EROR"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("BlogDetail", new { id = idBlog });
            }
        }

    }
}
