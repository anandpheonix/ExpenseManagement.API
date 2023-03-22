using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("[action]")]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }

    [HttpGet]
    [ActionName("categories")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var results = await _categoryRepository.GetCategories();

            return Ok(results);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    [ActionName("category")]
    public async Task<IActionResult> GetCategory(int id)
    {
        try
        {
            var result = await _categoryRepository.GetCategory(id);

            if (result is null) return NotFound();

            return Ok(result);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost]
    [ActionName("category/add")]
    public async Task<IActionResult> AddCategory(CategoryRequest request)
    {
        try
        {
            var category = new Categories()
            {
                Title = request.Title
            };

            category = await _categoryRepository.AddCategory(category);

            return CreatedAtAction(nameof(category), new { id = category.Id }, category);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPut]
    [ActionName("category/update")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest request)
    {
        try
        {
            var category = new Categories()
            {
                Title = request.Title
            };

            category = await _categoryRepository.UpdateCategory(id, category);

            if (category is null) return NotFound();

            return Ok(category);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpDelete]
    [ActionName("category/delete")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        try
        {
            var category = await _categoryRepository.DeleteCategory(id);

            if (category == null) return NotFound();

            return Ok(category);
        }
        catch (Exception)
        {
            throw;
        }
    }
}
