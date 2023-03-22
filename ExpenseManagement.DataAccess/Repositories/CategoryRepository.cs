using DataAccess.DBContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ExpensesContext _dbContext;

        public CategoryRepository(ExpensesContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Categories> AddCategory(Categories category)
        {
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<Categories>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Categories?> GetCategory(int id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Categories?> UpdateCategory(int id, Categories category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (existingCategory is null) 
            { 
                return null; 
            }

            existingCategory.Title = category.Title;

            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Categories?> DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category is null)
            {
                return null;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return category;
        }
    }
}
