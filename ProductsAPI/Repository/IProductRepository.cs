using ProductsAPI.DTOs;
using ProductsAPI.Models;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategories(int id);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> Get(int id);
    }
}
