using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIDWithProductsAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).Where(a => a.ID == id)
                .SingleOrDefaultAsync();
        }
    }
}
