using Microsoft.AspNetCore.Mvc;

namespace TFSMS.Admin.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
