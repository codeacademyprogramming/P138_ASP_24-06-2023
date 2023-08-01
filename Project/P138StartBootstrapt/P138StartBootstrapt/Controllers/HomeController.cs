using Microsoft.AspNetCore.Mvc;

namespace P138StartBootstrapt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
