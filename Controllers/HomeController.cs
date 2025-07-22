using Microsoft.AspNetCore.Mvc;

namespace DepoStok.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
