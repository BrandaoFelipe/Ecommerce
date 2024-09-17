using ProductsAPI.DTOs;
using ProductsAPI.Models;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category?>> GetAll();
        Task<Category?> Get(Expression<Func<Category, bool>> predicate);
    }
}
