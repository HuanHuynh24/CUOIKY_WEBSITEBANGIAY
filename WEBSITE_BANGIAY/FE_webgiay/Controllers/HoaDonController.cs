using FE_webgiay.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FE_webgiay.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseApiUrl = "https://localhost:7292/api/HoaDon";
        public HoaDonController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(_baseApiUrl); // Thay "your-api-endpoint" bằng URL API thật sự.

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", "Không thể tải dữ liệu hóa đơn từ API.");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<HoaDonViewModel>>(jsonData);

            return View(orders);
        }
    }
}
