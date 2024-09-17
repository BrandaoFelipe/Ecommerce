using Microsoft.EntityFrameworkCore;
using ProductsAPI.Context;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;


        public Repository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<T?> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Update(T entity)
        {
            _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
