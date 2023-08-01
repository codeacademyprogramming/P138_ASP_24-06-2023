using P138Alup.Models;
using P138Alup.ViewModels.BasketVMs;

namespace P138Alup.Interfaces
{
    public interface ILayoutService
    {
        Task<List<Setting>> GetSetting();
        Task<IEnumerable<Category>> GetCategories();
        Task<List<BasketVM>> GetBasket();
    }
}
