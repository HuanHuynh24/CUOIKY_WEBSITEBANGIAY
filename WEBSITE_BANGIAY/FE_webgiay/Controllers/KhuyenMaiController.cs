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
        //create
        [HttpPost]
        public async Task<IActionResult> Create(Khuyenmai newKhuyenMai)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newKhuyenMai), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Tạo khuyến mãi thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể thêm khuyến mãi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
        //edit
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var khuyenMai = JsonSerializer.Deserialize<Khuyenmai>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (khuyenMai == null)
                {
                    ViewBag.Error = "Không tìm thấy khuyến mãi.";
                    return RedirectToAction("Index");
                }

                return View(khuyenMai);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi tải khuyến mãi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Khuyenmai khuyenMai)
        {
            try
            {
                var km = new Khuyenmai
                {
                    MaKhuyenmai = khuyenMai.MaKhuyenmai.Trim(),
                    TenKhuyenmai = khuyenMai.TenKhuyenmai,
                    PhanTram = khuyenMai.PhanTram
                };

                var content = new StringContent(JsonSerializer.Serialize(km), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseApiUrl}/{khuyenMai.MaKhuyenmai}", content);
                /*var encodedId = Uri.EscapeDataString(khuyenMai.MaKhuyenmai.Trim());
                var response = await _httpClient.PutAsync($"{_baseApiUrl}/{encodedId}", content);*/
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật khuyến mãi thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể cập nhật khuyến mãi. Vui lòng thử lại.";
                return View(khuyenMai);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                return View(khuyenMai);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var khuyenMai = JsonSerializer.Deserialize<Khuyenmai>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (khuyenMai == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy khuyến mãi.";
                    return RedirectToAction("Index");
                }

                return View(khuyenMai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi tải khuyến mãi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Xoá khuyến mãi thành công!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Không thể xoá khuyến mãi.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xoá khuyến mãi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
