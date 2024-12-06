using FE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController()
        {
            _httpClient = new HttpClient();
        }
        // GET: Category
        public async Task<ActionResult> Category(int page = 1)
        {
            // Gọi cả 3 API
            List<DanhMucDH> danhMucList = await GetDanhmuc();
            List<BrandDH> brandList = await GetBrand();
            List<ColorDH> colorList = await GetColor();
            List<ChiTietSanPhamDH> sanphamList = await GetChiTietSanPham();

            // Số sản phẩm hiển thị mỗi trang
            int pageSize = 12;

            // Tính toán dữ liệu cho trang hiện tại
            int skip = (page - 1) * pageSize;
            List<ChiTietSanPhamDH> paginatedSanphamList = sanphamList.Skip(skip).Take(pageSize).ToList();

            // Tính tổng số trang
            int totalPages = (int)Math.Ceiling((double)sanphamList.Count / pageSize);

            // Gán vào ViewModel
            CategoryViewModel viewModel = new CategoryViewModel
            {
                DanhMucs = danhMucList,
                Brands = brandList,
                Colors = colorList,
                SanPhams = paginatedSanphamList,
                CurrentPage = page,
                TotalPages = totalPages
            };

            // Truyền ViewModel vào View
            return View(viewModel);
        }
        public async Task<ActionResult> SanPhamListPartial(int page = 1)
        {
            // Gọi cả 3 API
            List<DanhMucDH> danhMucList = await GetDanhmuc();
            List<BrandDH> brandList = await GetBrand();
            List<ColorDH> colorList = await GetColor();
            List<ChiTietSanPhamDH> sanphamList = await GetChiTietSanPham();

            // Số sản phẩm hiển thị mỗi trang
            int pageSize = 12;

            // Tính toán dữ liệu cho trang hiện tại
            int skip = (page - 1) * pageSize;
            List<ChiTietSanPhamDH> paginatedSanphamList = sanphamList.Skip(skip).Take(pageSize).ToList();

            // Tính tổng số trang
            int totalPages = (int)Math.Ceiling((double)sanphamList.Count / pageSize);

            // Gán vào ViewModel
            CategoryViewModel viewModel = new CategoryViewModel
            {
                DanhMucs = danhMucList,
                Brands = brandList,
                Colors = colorList,
                SanPhams = paginatedSanphamList,
                CurrentPage = page,
                TotalPages = totalPages
            };

            // Truyền ViewModel vào View
            return View(viewModel);
        }



        [HttpGet]
        public async Task<List<DanhMucDH>> GetDanhmuc()
        {
            try
            {
                string apiUrl = "http://localhost:5288/api/DanhMuc";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<DanhMucDH> danhMucList = JsonConvert.DeserializeObject<List<DanhMucDH>>(jsonResponse);
                    return danhMucList;  // Trả về danh sách danh mục
                }
                else
                {
                    // Xử lý khi API không thành công
                    return new List<DanhMucDH>();  // Trả về danh sách rỗng nếu API thất bại
                }
            }
            catch (Exception ex)
            {
                // Xử lý khi có lỗi trong quá trình gọi API
                return new List<DanhMucDH>();  // Trả về danh sách rỗng nếu có lỗi
            }
        }

        [HttpGet]
        public async Task<List<BrandDH>> GetBrand()
        {
            try
            {
                string apiBrandUrl = "http://localhost:5288/api/Brand";
                HttpResponseMessage response = await _httpClient.GetAsync(apiBrandUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<BrandDH> brandList = JsonConvert.DeserializeObject<List<BrandDH>>(jsonResponse);
                    return brandList;  // Trả về danh sách danh mục
                }
                else
                {
                    // Xử lý khi API không thành công
                    return new List<BrandDH>();  // Trả về danh sách rỗng nếu API thất bại
                }
            }
            catch (Exception ex)
            {
                return new List<BrandDH>();
            }
        }

        [HttpGet]
        public async Task<List<ColorDH>> GetColor()
        {
            try
            {
                string apiBrandUrl = "http://localhost:5288/api/ColorDH";
                HttpResponseMessage response = await _httpClient.GetAsync(apiBrandUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ColorDH> colorList = JsonConvert.DeserializeObject<List<ColorDH>>(jsonResponse);
                    return colorList;  // Trả về danh sách danh mục
                }
                else
                {
                    // Xử lý khi API không thành công
                    return new List<ColorDH>();  // Trả về danh sách rỗng nếu API thất bại
                }
            }
            catch (Exception ex)
            {
                return new List<ColorDH>();
            }
        }

        [HttpGet]
        public async Task<List<ChiTietSanPhamDH>> GetChiTietSanPham()
        {
            try
            {
                string apiSanPhamUrl = "http://localhost:5288/api/ChitietsanphamDH";
                HttpResponseMessage response = await _httpClient.GetAsync(apiSanPhamUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ChiTietSanPhamDH> sanphamList = JsonConvert.DeserializeObject<List<ChiTietSanPhamDH>>(jsonResponse);
                    return sanphamList;  // Trả về danh sách sản phẩm
                }
                else
                {
                    // Xử lý khi API không thành công
                    return new List<ChiTietSanPhamDH>();  // Trả về danh sách rỗng nếu API thất bại
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log lỗi
                return new List<ChiTietSanPhamDH>();
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsByCategory(int maDanhMuc)
        {
            try
            {
                string apiUrl = $"http://localhost:5288/api/ChitietsanphamDH/byDanhmuc/{maDanhMuc}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ChiTietSanPhamDH> sanphamList = JsonConvert.DeserializeObject<List<ChiTietSanPhamDH>>(jsonResponse);
                    return Json(sanphamList, JsonRequestBehavior.AllowGet); // Trả về dữ liệu JSON
                }
                else
                {
                    return Content("Không có sản phẩm nào.");
                }
            }
            catch (Exception ex)
            {
                return Content("Lỗi: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetFilterByBrand(int maNhan)
        {
            try
            {
                string apiUrl = $"http://localhost:5288/api/ChitietsanphamDH/filter-by-nhanhieu/{maNhan}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ChiTietSanPhamDH> sanphamList = JsonConvert.DeserializeObject<List<ChiTietSanPhamDH>>(jsonResponse);
                    return Json(sanphamList, JsonRequestBehavior.AllowGet); // Trả về dữ liệu JSON
                }
                else
                {
                    return Content("Không có sản phẩm nào.");
                }
            }
            catch (Exception ex)
            {
                return Content("Lỗi: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetFilterByColor(int maMau)
        {
            try
            {
                string apiUrl = $"http://localhost:5288/api/ChitietsanphamDH/filter-by-color/{maMau}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<ChiTietSanPhamDH> sanphamList = JsonConvert.DeserializeObject<List<ChiTietSanPhamDH>>(jsonResponse);
                    return Json(sanphamList, JsonRequestBehavior.AllowGet); // Trả về dữ liệu JSON
                }
                else
                {
                    return Content("Không có sản phẩm nào.");
                }
            }
            catch (Exception ex)
            {
                return Content("Lỗi: " + ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetByPriceRange(int gia1, int gia2)
        {
            try
            {
                // Tạo URL với các tham số minPrice và maxPrice
                string apiUrl = $"http://localhost:5288/api/ChitietsanphamDH/by-price-range?minPrice={gia1}&maxPrice={gia2}";

                // Gửi yêu cầu GET đến API
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Đọc phản hồi từ API
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Deserialize dữ liệu JSON thành danh sách sản phẩm
                    List<ChiTietSanPhamDH> sanphamList = JsonConvert.DeserializeObject<List<ChiTietSanPhamDH>>(jsonResponse);

                    // Trả về kết quả dưới dạng JSON
                    return Json(sanphamList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Trả về thông báo lỗi nếu không có sản phẩm
                    return Content("Không có sản phẩm nào.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có sự cố
                return Content("Lỗi: " + ex.Message);
            }
        }


    }
}