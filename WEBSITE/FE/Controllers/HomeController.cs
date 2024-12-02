using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FE.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public async Task<ActionResult> Chitietsanpham(string masanpham, int macolor, int masize)
        {
            // Đảm bảo chuỗi mã sản phẩm không vượt quá 8 ký tự
            masanpham = masanpham.Length > 8 ? masanpham.Substring(0, 8) : masanpham;

            // Gửi yêu cầu GET đến API
            var response = await client.GetAsync($"http://localhost:5288/api/chitietsanpham/chitietsanpham?masanpham={masanpham}&color={macolor}&size={masize}");

            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu JSON từ API
                var json = await response.Content.ReadAsStringAsync();

                // Deserialize JSON thành dynamic để xử lý dữ liệu
                dynamic data = JsonConvert.DeserializeObject(json);

                // Kiểm tra và lấy thông tin sản phẩm
                var sanpham = data?.data?.chitietSanpham?.maSanphamNavigation;
                var chitiet = data?.data?.chitietSanpham;
                var reviews = data?.data?.reviews;
                List<danhgia> danhGiaList = new List<danhgia>();
                if (reviews != null)
                {
                    // Chuyển từ dynamic thành List<DanhGia>
                    danhGiaList = JsonConvert.DeserializeObject<List<danhgia>>(reviews.ToString());
                    ViewBag.danhgial = danhGiaList;
                }

                    ViewBag.tensanpham = sanpham?.tenSanpham;
                    ViewBag.mota = sanpham?.moTa;
                    ViewBag.gia = chitiet.gia;
                    ViewBag.masapham = sanpham.maSanpham;


                // Trả về View với thông tin sản phẩm
                return View();
               
            }

            // Trường hợp yêu cầu không thành công
            return HttpNotFound();
        }


        public ActionResult Search(string masanpham)
        {
            return View();

        }


     


        public async Task<ActionResult> CallApiWithoutModel()
        {
            string apiUrl = "http://localhost:5288/SanPham";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return Content(data); // Dữ liệu dạng thô
                }
                else
                {
                    return Content($"Lỗi khi gọi API: {response.StatusCode}");
                }
            }
        }



    }
}