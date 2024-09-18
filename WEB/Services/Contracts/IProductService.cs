using WEB.Models;

namespace WEB.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsViewModel>> GetAllAsync(string token);
        Task<ProductsViewModel> GetByIdAsync(int id, string token);
        Task<ProductsViewModel> CreateProduct(ProductsViewModel productVm, string token);
        Task<ProductsViewModel> UpdateProductAsync(ProductsViewModel productVm, string token);
        Task<bool> DeleteProductById(int id, string token);
    }
}
