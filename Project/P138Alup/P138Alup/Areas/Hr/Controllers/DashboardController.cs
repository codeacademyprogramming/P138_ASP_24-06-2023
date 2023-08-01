using Microsoft.AspNetCore.Mvc;

namespace P138Alup.Areas.Hr.Controllers
{
    [Area("hr")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return Content("Welcoe To HR");
        }
    }
}
