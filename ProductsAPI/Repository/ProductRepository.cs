using Microsoft.EntityFrameworkCore;
using ProductsAPI.Context;
using ProductsAPI.Models;

namespace ProductsAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCategories(int id)
        {
            var prod = await GetAll();

            var prodByCategory = prod
                .Where(p => p.CategoryId == id);

            return prodByCategory;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Set<Product>()
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Set<Product>()
                .Include(c => c.Category)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
