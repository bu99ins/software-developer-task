using System.Web.Mvc;

namespace SalesReport.API.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
