using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<Categories> AddCategory(Categories category);

        Task<IEnumerable<Categories>> GetCategories();

        Task<Categories> GetCategory(int id);

        Task<Categories> UpdateCategory(int id, Categories category);

        Task<Categories> DeleteCategory(int id);

    }
}
