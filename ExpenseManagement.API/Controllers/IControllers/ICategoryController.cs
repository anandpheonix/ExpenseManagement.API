using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.IControllers
{
    public interface ICategoryController
    {
        Task<IActionResult> AddCategory([FromBody] CategoryRequest request);
        Task<IActionResult> DeleteCategory(int id);
        Task<IActionResult> GetCategories(CancellationToken cancellationToken);
        Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken);
        Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request);
    }
}
