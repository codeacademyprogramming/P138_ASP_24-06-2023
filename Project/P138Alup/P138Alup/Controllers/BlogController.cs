using Microsoft.AspNetCore.Mvc;

namespace P138Alup.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Page = 4;
            return View();
        }
    }
}
