using FE.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FE.Controllers
{
    public class ShopController : Controller
    {
      

        public async Task<ActionResult> Cart()
        {
            if (Request.Cookies["idUser"] != null)
            {
                var cookieValue = Request.Cookies["idUser"].Value;
                string url = "http://localhost:5288/ChitiethoadonH/giohang/" + cookieValue;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();

                        List<GioHangItem> carts = JsonConvert.DeserializeObject<List<GioHangItem>>(data);

                        return View(carts);
                    }
                    else
                    {
                        return Content($"Lỗi khi gọi API: {response.StatusCode}");
                    }
                }
            }
            else TempData["THONGBAO"] = "Hãy đăng nhập để xem giỏ hàng";

            return View();
        }
    }
}