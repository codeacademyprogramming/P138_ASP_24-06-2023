using Microsoft.AspNetCore.Mvc;

namespace P138StartBootstrapt.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
