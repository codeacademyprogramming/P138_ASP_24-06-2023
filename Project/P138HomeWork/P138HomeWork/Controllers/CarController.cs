using Microsoft.AspNetCore.Mvc;
using P138HomeWork.Models;

namespace P138HomeWork.Controllers
{
    public class CarController : Controller
    {
        private readonly List<Car> _cars;

        public CarController()
        {
            _cars = new List<Car> { 
                new Car { Id = 1,Color="Yasil",Year="1990",ModelId=1},
                new Car { Id = 2,Color="Qirmizi",Year="1990",ModelId=1},
                new Car { Id = 3,Color="Qara",Year="1990",ModelId=2},
                new Car { Id = 4,Color="Ag",Year="1990",ModelId=2},
                new Car { Id = 5,Color="BozDeyil",Year="1990",ModelId=3},
                new Car { Id = 6,Color="Sari",Year="1990",ModelId=3},
                new Car { Id = 7,Color="Benevsoyi",Year="1990",ModelId=4},
                new Car { Id = 8,Color="Mavi",Year="1990",ModelId=4}
            };
        }

        public IActionResult Index(int? id)
        {
            if(id==null) return BadRequest();

            if(!_cars.Exists(c=>c.ModelId == id)) return NotFound();

            return View(_cars.FindAll(c=>c.ModelId == id));
        }


        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();

            if (!_cars.Exists(c => c.Id == id)) return NotFound();

            return View(_cars.Find(c => c.Id == id));
        }
    }
}
