using Microsoft.AspNetCore.Mvc;
using P138HomeWork.Models;

namespace P138HomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<Marka> _markas;

        public HomeController()
        {
            _markas = new List<Marka> { 
                new Marka { Id = 1,Name="BMW"},
                new Marka { Id = 2,Name="Mercedes"}
            };
        }

        public IActionResult Index()
        {
            return View(_markas);
        }
    }
}
