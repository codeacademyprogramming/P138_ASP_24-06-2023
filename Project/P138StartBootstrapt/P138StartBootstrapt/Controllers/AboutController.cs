using Microsoft.AspNetCore.Mvc;

namespace P138StartBootstrapt.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
