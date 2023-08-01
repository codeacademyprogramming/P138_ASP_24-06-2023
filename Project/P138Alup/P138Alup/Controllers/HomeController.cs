using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Alup.DataAccessLayer;
using P138Alup.Services;
using P138Alup.ViewModels.HomeVMs;

namespace P138Alup.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {


            //ViewBag.Page = 1;
            HomeVM homeVM = new HomeVM
            {
                
                Categories = await _context.Categories.Where(c => c.IsDeleted == false && c.IsMain).ToListAsync(),
                NewArrivals = await _context.Products.Where(p => p.IsDeleted == false && p.IsNewArrival).ToListAsync(),
                BestSeller = await _context.Products.Where(p => p.IsDeleted == false && p.IsBestSeller).ToListAsync(),
                Featured = await _context.Products.Where(p => p.IsDeleted == false && p.IsFeatured).ToListAsync()
            };


            return View(homeVM);
        }

        //public IActionResult SetCockie()
        //{
        //    Response.Cookies.Append("P138Cocckie", "Hello World P138 Cookie");

        //    return Ok();
        //}

        //public IActionResult GetCookie()
        //{
        //    string? cookie = Request.Cookies["P138Cocckie"];

        //    return Ok(cookie);
        //}

        //public async Task<IActionResult> SetSession()
        //{
        //    HttpContext.Session.SetString("P138", "Hello World P138");

        //    return Ok();
        //}

        //public async Task<IActionResult> GetSession()
        //{
        //    string? sess = HttpContext.Session.GetString("P138");

        //    if (sess != null)
        //    {
        //        return Ok(sess);
        //    }
        //    else
        //    {
        //        return Ok("Session Silinib");
        //    }
        //}
    }
}
