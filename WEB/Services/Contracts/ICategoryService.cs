using WEB.Models;

namespace WEB.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync(string token);
        Task<CategoryViewModel> GetByIdAsync(int id, string token);
        Task<CategoryViewModel> CreateCategory(CategoryViewModel categoryVM, string token);
        Task<CategoryViewModel> UpdateCategoryAsync(CategoryViewModel categoryVM, string token);
        Task<bool> DeleteCategoryById(int id, string token);
    }
}
