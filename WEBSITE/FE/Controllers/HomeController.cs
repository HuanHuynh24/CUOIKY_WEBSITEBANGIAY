
using FE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace FE.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();


        public async Task<ActionResult> Index()
        {

            string apiUrl = "http://localhost:5288/api/SanphamH";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON thành danh sách các đối tượng SanPham
                    List<NhanhieuH> sanPhamList = JsonConvert.DeserializeObject<List<NhanhieuH>>(data);

                    // Truyền danh sách sản phẩm vào View
                    return View(sanPhamList);
                }
                else
                {
                    return Content($"Lỗi khi gọi API: {response.StatusCode}");
                }
            }
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
                var size = data?.data?.chitietSanpham?.maSizeNavigation;
                var chitiet = data?.data?.chitietSanpham;
                var hinhanhs = data?.data?.hinhanhs;
                var reviews = data?.data?.reviews;
                List<danhgia> danhGiaList = new List<danhgia>();
                List<Hinhanh> hinhAnhList = new List<Hinhanh>();

                    // Chuyển từ dynamic thành List<DanhGia>
                    danhGiaList = JsonConvert.DeserializeObject<List<danhgia>>(reviews.ToString());
                    ViewBag.danhgial = danhGiaList;
           
              
                    // Chuyển từ dynamic thành List<DanhGia>
                    hinhAnhList = JsonConvert.DeserializeObject<List<Hinhanh>>(hinhanhs.ToString());
                    ViewBag.hinhanhl = hinhAnhList;
                

                ViewBag.tensanpham = sanpham?.tenSanpham;
                ViewBag.mota = sanpham?.moTa;
                ViewBag.gia = chitiet.gia;
                ViewBag.masapham = sanpham.maSanpham;


                ViewBag.masize=size.maSize;
                ViewBag.chieudai = size.chieudai;
                ViewBag.chieurong = size.chieurong;
                ViewBag.chieucao = size.chieucao;


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


    }
}