using FE_webgiay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FE_webgiay.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        private readonly HttpClient _httpClient;

        public ChiTietSanPhamController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7292/api/ChiTietSanPham");
                response.EnsureSuccessStatusCode();  // Kiểm tra nếu API trả về lỗi
                var content = await response.Content.ReadAsStringAsync();
                var chiTietSanPhams = JsonConvert.DeserializeObject<IEnumerable<ChiTietSanPhamViewModel>>(content);

                return View(chiTietSanPhams);  // Truyền dữ liệu vào View
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra khi gọi API: " + ex.Message;
                return View("Error");  // Hiển thị trang lỗi nếu có ngoại lệ
            }

        }
    }
}
