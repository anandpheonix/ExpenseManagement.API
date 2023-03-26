using Application.Controllers.Interfaces;
using Common;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : Controller, ICategoryController
{
    protected APIResponse _response;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
        _response = new();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
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

    [HttpGet]
    [ResponseCache(CacheProfileName = "DefaultGet")]
    [ProducesResponseType(typeof(Categories), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        try
        {
            var results = await _categoryRepository.GetCategories(cancellationToken);

            if (results is null)
            {
                _response.Data = null;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            _response.Data = results;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(results);
        }
        catch (OperationCanceledException)
        {

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }

    [HttpGet("{id}")]
    [ResponseCache(CacheProfileName = "DefaultGet")]
    public async Task<IActionResult> GetCategory(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _categoryRepository.GetCategory(id, cancellationToken);

            if (result is null) return NotFound();

            return Ok(result);
        }
        catch (OperationCanceledException)
        {
        
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
