using FE_webgiay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FE_webgiay.Controllers
{
    public class NhanHieuController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl = "https://localhost:7292/api/ThuongHieu";
        public NhanHieuController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Hiển thị danh sách thương hiệu
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseApiUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var nhanHieus = JsonSerializer.Deserialize<IEnumerable<Nhanhieu>>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var viewModel = new NhanHieuViewModel
                {
                    NhanHieus = nhanHieus ?? new List<Nhanhieu>(),
                    NewNhanHieu = new Nhanhieu()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể tải danh sách thương hiệu: " + ex.Message;
                return View(new NhanHieuViewModel
                {
                    NhanHieus = new List<Nhanhieu>(),
                    NewNhanHieu = new Nhanhieu()
                });
            }
        }

        //create thương hiệu
        [HttpPost]
        public async Task<IActionResult> Create(Nhanhieu newNhanHieu)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(newNhanHieu), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Thêm thương hiệu thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể thêm thương hiệu.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var nhanHieu = JsonSerializer.Deserialize<Nhanhieu>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (nhanHieu == null)
                {
                    ViewBag.Error = "Không tìm thấy nhãn hiệu.";
                    return RedirectToAction("Index");
                }

                return View(nhanHieu);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi khi tải nhãn hiệu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Nhanhieu nhanHieu)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(nhanHieu), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseApiUrl}/{nhanHieu.MaNhan}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Cập nhật thương hiệu thành công!";
                    return RedirectToAction("Index");
                }

                ViewBag.Error = "Không thể cập nhật thương hiệu. Vui lòng thử lại.";
                return View(nhanHieu);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Đã xảy ra lỗi: " + ex.Message;
                return View(nhanHieu);
            }
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}/{id}");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var nhanHieu = JsonSerializer.Deserialize<Nhanhieu>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (nhanHieu == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thương hiệu.";
                    return RedirectToAction("Index");
                }

                return View(nhanHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi tải thương hiệu: " + ex.Message;
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
                    TempData["SuccessMessage"] = "Xoá thương hiệu thành công!";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Không thể xoá thương hiệu.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xoá thương hiệu: " + ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
