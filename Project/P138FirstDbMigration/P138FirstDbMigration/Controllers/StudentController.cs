using Microsoft.AspNetCore.Mvc;
using P138FirstDbMigration.DataAccessLayer;
using P138FirstDbMigration.Models;

namespace P138FirstDbMigration.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id == null) return BadRequest();

            if(!_context.Students.Any(s=>s.GroupId == id)) return NotFound();

            return View(_context.Students.Where(s=>s.GroupId == id).ToList());
        }

        public IActionResult Detail(int? id)
        {
            if (id == null) return BadRequest();

            Student student = _context.Students.FirstOrDefault(s=>s.Id == id);

            if (student == null) return NotFound();

            return View(student);
        }
    }
}
