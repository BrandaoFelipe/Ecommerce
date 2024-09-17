using Microsoft.EntityFrameworkCore;
using ProductsAPI.Context;
using ProductsAPI.Models;
using System.Linq.Expressions;

namespace ProductsAPI.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category?>> GetAll()
        {
            return await _context.Set<Category>().ToListAsync();
        }

        public async Task<Category?> Get(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Set<Category>().FirstOrDefaultAsync(predicate);
        }
    }
}
