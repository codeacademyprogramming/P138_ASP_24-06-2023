using Microsoft.AspNetCore.Mvc;
using P138FirstDbMigration.DataAccessLayer;

namespace P138FirstDbMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Groups.ToList());
        }
    }
}
