using VShopWEB.Models;

namespace VShopWEB.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel> GetByIdAsync(int id);
        Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM);
        Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel categoryVM);
        Task<bool> DeleteCategoryById(int id);
    }
}
