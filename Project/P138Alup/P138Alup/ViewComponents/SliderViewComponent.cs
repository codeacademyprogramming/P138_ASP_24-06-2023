using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P138Alup.DataAccessLayer;
using P138Alup.Models;

namespace P138Alup.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SliderViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _context.Sliders.Where(s=>s.IsDeleted == false).ToListAsync();

            return View(sliders);
        }
    }
}
