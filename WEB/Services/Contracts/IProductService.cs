using VShopWEB.Models;

namespace VShopWEB.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductsViewModel>> GetAllAsync();
        Task<ProductsViewModel> GetByIdAsync(int id);
        Task<ProductsViewModel> CreateProduct(ProductsViewModel productVm);
        Task<ProductsViewModel> UpdateProductAsync(ProductsViewModel productVm);
        Task<bool> DeleteProductById(int id);
    }
}
