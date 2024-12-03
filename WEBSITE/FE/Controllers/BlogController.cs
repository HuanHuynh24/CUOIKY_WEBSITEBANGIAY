using System.Net.Http;
using System.Web.Mvc;

namespace FE.Controllers
{
	public class BlogController : Controller
	{
        private readonly HttpClient _httpClient;

        public BlogController()
        {
            _httpClient = new HttpClient();

        }
        public ActionResult Blog()
		{
			var user = Session["UserSession"] as User;
			if (user == null)
			{
                TempData["EROR"] = "Bạn phải đăng nhập trước.";
                return RedirectToAction("Login", "Account");
            }
			return View();
		}
	}
}
