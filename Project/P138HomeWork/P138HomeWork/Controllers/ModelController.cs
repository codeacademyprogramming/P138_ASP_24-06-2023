using Microsoft.AspNetCore.Mvc;
using P138HomeWork.Models;

namespace P138HomeWork.Controllers
{
    public class ModelController : Controller
    {
        private readonly List<Model> _models;

        public ModelController()
        {
            _models = new List<Model> { 
                new Model { Id = 1,Name="M5",MarkaId=1},
                new Model { Id = 2,Name="X5",MarkaId=1},
                new Model { Id = 3,Name="A-Class",MarkaId=2},
                new Model { Id = 4,Name="CLS",MarkaId=2},
            };
        }

        public IActionResult Index(int? id)
        {
            if (id == null) return BadRequest();

            if(!_models.Exists(x => x.MarkaId == id)) return NotFound();

            return View(_models.FindAll(m=>m.MarkaId == id));
        }
    }
}
