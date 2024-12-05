using FE_webgiay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FE_webgiay.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7292/api/KhuyenMai"; // Thay bằng URL API thực tế của bạn

        public KhuyenMaiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseApiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var khuyenMais = JsonSerializer.Deserialize<IEnumerable<Khuyenmai>>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var viewModel = new KhuyenMaiViewModel
                {
                    KhuyenMais = khuyenMais ?? new List<Khuyenmai>(),
                    NewKhuyenMai = new Khuyenmai()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể tải danh sách khuyến mãi: " + ex.Message;
                return View(new KhuyenMaiViewModel
                {
                    KhuyenMais = new List<Khuyenmai>(),
                    NewKhuyenMai = new Khuyenmai()
                });
            }
        }
    }
}
