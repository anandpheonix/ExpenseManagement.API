using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Interfaces
{
    public interface ICategoryController
    {
        Task<IActionResult> AddCategory([FromBody] CategoryRequest request);
        Task<IActionResult> DeleteCategory(int id);
        Task<IActionResult> GetCategories();
        Task<IActionResult> GetCategory(int id);
        Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request);
    }
}
