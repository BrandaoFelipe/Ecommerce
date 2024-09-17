using ProductsAPI.DTOs;

namespace ProductsAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetByCategories(int id);
        Task<IEnumerable<ProductDTO>> GetAll();
        Task<ProductDTO> GetById(int id);
        Task AddProduct(ProductDTO productDTO);
        Task UpdateProduct(ProductDTO productDTO);
        Task DeleteProduct(int id);
    }
}
