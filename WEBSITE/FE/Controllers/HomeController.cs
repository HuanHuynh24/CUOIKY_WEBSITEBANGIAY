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


        public async Task<ActionResult> Index()
        {

            string apiUrl = "http://localhost:5288/api/SanphamH";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    List<NhanhieuH> sanphamList = JsonConvert.DeserializeObject<List<NhanhieuH>>(data);
                    return View(sanphamList);
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

       
    }
}