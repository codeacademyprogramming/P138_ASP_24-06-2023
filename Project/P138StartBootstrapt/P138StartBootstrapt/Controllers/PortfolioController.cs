using Microsoft.AspNetCore.Mvc;

namespace P138StartBootstrapt.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
