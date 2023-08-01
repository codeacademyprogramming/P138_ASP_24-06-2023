using Microsoft.AspNetCore.Mvc;

namespace P138Alup.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Page = 2;
            return View();
        }
    }
}
