using DataAccess.Models;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<Categories> AddCategory(Categories category);

        Task<IEnumerable<Categories>> GetCategories(CancellationToken cancellationToken);

        Task<Categories> GetCategory(int id, CancellationToken cancellationToken);

        Task<Categories> UpdateCategory(int id, Categories category);

        Task<Categories> DeleteCategory(int id);

    }
}
