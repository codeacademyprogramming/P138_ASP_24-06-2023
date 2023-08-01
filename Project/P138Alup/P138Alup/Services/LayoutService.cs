using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P138Alup.DataAccessLayer;
using P138Alup.Interfaces;
using P138Alup.Models;
using P138Alup.ViewModels.BasketVMs;

namespace P138Alup.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public LayoutService(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }

        public async Task<List<BasketVM>> GetBasket()
        {
            string? cookie = _contextAccessor.HttpContext.Request.Cookies["basket"];

            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);

                foreach (BasketVM basketVM in basketVMs)
                {
                    Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.Id);

                    basketVM.Image = product.MainImage;
                    basketVM.ExTax = product.ExTax;
                    basketVM.Price = product.DiscountedPrice > 0 ? product.DiscountedPrice : product.Price;
                    basketVM.Title = product.Title;
                }
            }
            else
            {
                basketVMs= new List<BasketVM>();
            }

            return basketVMs;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.Include(c=>c.Children.Where(c=>c.IsDeleted == false))
                .Where(c=>c.IsDeleted == false && c.IsMain).ToListAsync();
        }

        public async Task<List<Setting>> GetSetting() 
        {
            List<Setting> settings= await _context.Settings.ToListAsync();

            return settings;
        }
    }
}
