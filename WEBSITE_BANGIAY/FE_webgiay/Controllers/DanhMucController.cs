using FE_webgiay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
namespace FE_webgiay.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7292/api/DanhMuc"; // Thay bằng URL API thực tế của bạn

        public DanhMucController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseApiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var danhMucs = JsonSerializer.Deserialize<IEnumerable<Danhmuc>>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var viewModel = new DanhMucViewModel
                {
                    DanhMucs = danhMucs ?? new List<Danhmuc>(),
                    NewDanhMuc = new Danhmuc()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể tải danh sách danh mục: " + ex.Message;
                return View(new DanhMucViewModel
                {
                    DanhMucs = new List<Danhmuc>(),
                    NewDanhMuc = new Danhmuc()
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Danhmuc newDanhMuc)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newDanhMuc), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Tạo danh mục thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể thêm danh mục.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var danhMuc = JsonSerializer.Deserialize<Danhmuc>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (danhMuc == null)
                {
                    ViewBag.Error = "Không tìm thấy danh mục.";
                    return RedirectToAction("Index");
                }

                return View(danhMuc);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi tải danh mục: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Danhmuc danhMuc)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(danhMuc), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseApiUrl}/{danhMuc.MaDanhmuc}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể cập nhật danh mục. Vui lòng thử lại.";
                return View(danhMuc);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                return View(danhMuc);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var danhMuc = JsonSerializer.Deserialize<Danhmuc>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (danhMuc == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy danh mục.";
                    return RedirectToAction("Index");
                }

                return View(danhMuc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi tải danh mục: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Xoá danh mục thành công!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Không thể xoá danh mục.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xoá danh mục: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
