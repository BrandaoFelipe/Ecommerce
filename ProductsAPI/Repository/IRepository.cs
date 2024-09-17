using ProductsAPI.Context;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        
        
        Task<T?> Create(T entity);
        Task<T?> Update(T entity);
        Task<T?> Delete(T entity);

    }
}
