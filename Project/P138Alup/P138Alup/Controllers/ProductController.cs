using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Alup.DataAccessLayer;
using P138Alup.Models;

namespace P138Alup.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search(int? id, string search)
        {
            if (id != null && id > 0)
            {
                if(!await _context.Categories.AnyAsync(c=>c.IsDeleted == false && c.Id == id))
                {
                    return BadRequest($"Category Id = {id} Yanlisdir");
                }

                List<Product> products = await _context.Products.Where(c=>c.IsDeleted == false && c.CategoryId == id &&(
                c.Title.ToLower().Contains(search.ToLower()) ||
                (c.Brand != null ? c.Brand.Name.ToLower().Contains(search.ToLower()) : true) ||
                (c.Category != null ? c.Category.Name.ToLower().Contains(search.ToLower()) : true) ||
                c.Description.ToLower().Contains(search.ToLower()))).ToListAsync();

                //return Json(products);
                return PartialView("_SearchPartial", products);
            }
            else
            {
                List<Product> products = await _context.Products.Where(c => c.IsDeleted == false && (
                c.Title.ToLower().Contains(search.ToLower()) ||
                (c.Brand != null ? c.Brand.Name.ToLower().Contains(search.ToLower()) : true) ||
                (c.Category != null ? c.Category.Name.ToLower().Contains(search.ToLower()) : true) ||
                c.Description.ToLower().Contains(search.ToLower()))).ToListAsync();

                return PartialView("_SearchPartial", products);
            }
        }

        public async Task<IActionResult> Modal(int? id)
        {
            if(id == null) return BadRequest();

            Product product = await _context.Products.Include(p=>p.ProductImages.Where(pi=>pi.IsDeleted == false))
                .FirstOrDefaultAsync(p=>p.IsDeleted == false && p.Id == id);

            if(product == null) return NotFound();

            return PartialView("_ModalPartial", product);
        }
    }
}
