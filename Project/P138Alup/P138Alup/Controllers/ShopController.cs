using Microsoft.AspNetCore.Mvc;

namespace P138Alup.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Page = 3;
            return View();
        }
    }
}
