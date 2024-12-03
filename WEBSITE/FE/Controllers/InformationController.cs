using System.Web.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FE.Controllers
{
	public class InformationController : Controller
	{
		private readonly HttpClient httpClient;

		public InformationController()
		{
			httpClient = new HttpClient();

		}
		public ActionResult Information()
		{

			return View();
        }

	}
}
