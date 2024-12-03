using System.Web.Mvc;
using System;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using System.Text.Json;


namespace FE.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;

		public AccountController()
		{
			_httpClient = new HttpClient();

		}
			[HttpPost]
		public async Task<ActionResult> GetUser(string Taikhoan,string Matkhau)
		{
			var url = $"http://localhost:5288/api/UsersControllerK?Taikhoan={Taikhoan}";
			if (string.IsNullOrEmpty(Taikhoan) || string.IsNullOrEmpty(Matkhau))
			{
				 TempData["EROR"] ="Tài khoản hoặc mật khẩu không được để trống.";
				 Console.WriteLine("TempData EROR: " + TempData["EROR"]);
				 return RedirectToAction("Login");
			}
			
			try
			{
				var loginUser = new User { Taikhoan = Taikhoan, Matkhau = Matkhau };
                var content = new StringContent(JsonConvert.SerializeObject(loginUser), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url,content);

				response.EnsureSuccessStatusCode();

				if (!response.IsSuccessStatusCode)
				{
					return ViewBag.EROR = "API lỗi :" +response.ReasonPhrase ;
				}
				// Đọc dữ liệu từ API
				var jsonData = await response.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(jsonData, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
                if (user != null) {
					if (user.Matkhau.Equals(Matkhau) && user.Taikhoan.Equals(Taikhoan))
					{
                        //return RedirectToAction("Information", "Account");
                        Session["UserSession"] = user;
                        return View("Information", user);


					}
					else
					{
						 TempData["EROR"] = "Sai mật khẩu" ;
						return RedirectToAction("Login");
					}
					

				}
				else
				{
					TempData["EROR"] = " Không tồn tại tài khoản người dùng";
					return RedirectToAction("Login");
				}
				
			}

			catch (HttpRequestException ex)
			{
				return ViewBag.EROR = "lỗi khi gọi API" + ex.Message;
			}
		}
		public ActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public async Task<ActionResult> Register(string Taikhoan, string Matkhau, string Ten, string Sdt, string Email, string DiaChi)
        {
            var url = "http://localhost:5288/Register";

            if (string.IsNullOrEmpty(Taikhoan) || string.IsNullOrEmpty(Matkhau) || string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(Sdt) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(DiaChi))
            {
                TempData["ERROR"] = "Vui lòng điền đầy đủ thông tin.";
                return RedirectToAction("Register");
            }

            try
            {
                var newUser = new RegisterUser
                {
                    Taikhoan = Taikhoan,
                    Matkhau = Matkhau, // Nên mã hóa mật khẩu tại backend
                    Ten = Ten,
                    Sdt = Sdt,
                    Email = Email,
                    DiaChi = DiaChi
                };

                var content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SUCCESS"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["ERROR"] = $"Đăng ký thất bại. API trả về lỗi: {response.ReasonPhrase}";
                    return RedirectToAction("Register");
                }
            }
            catch (HttpRequestException ex)
            {
                TempData["ERROR"] = $"Lỗi khi gọi API: {ex.Message}";
                return RedirectToAction("Register");
            }
        }


        public ActionResult Register()
        {
            return View();
        }

        public class RegisterUser
        {
            public string Taikhoan { get; set; }
            public string Matkhau { get; set; }
            public string Ten { get; set; }
            public string Sdt { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
        }

        public ActionResult Information()
        {
            var user = Session["UserSession"] as User;
            if (user == null)
            {
                TempData["EROR"] = "Bạn phải đăng nhập trước.";
                return RedirectToAction("Login", "Account");
            }
            return View(user);
        }
    }
}
