using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Alup.DataAccessLayer;
using P138Alup.Models;

namespace P138Alup.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Category> categories)
        {
            //IEnumerable<Category> categories = await _context.Categories.Where(c=>c.IsDeleted == false && c.IsMain).ToListAsync();

            return View(categories);
        }
    }
}
