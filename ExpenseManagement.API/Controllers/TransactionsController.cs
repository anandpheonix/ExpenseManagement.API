using Application.Controllers.IControllers;
using AutoMapper;
using Common;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Objects;
using DataTransfer.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase, ITransactionsController
{
    #region Global Object Declaration
    protected APIResponse _response;
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    #endregion 

    public TransactionsController(ITransactionRepository repository, IMapper mapper, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _response = new();
    }

    [HttpPost]
    [Authorize(Roles = "Write")]
    public async Task<IActionResult> AddTransaction([FromBody] ExpenseRequest request)
    {
        try
        {
            var transaction = new Transactions()
            {
                Item = request.Item,
                Amount = request.Amount,
                Comment = request.Comment,
                CategoryId = request.CategoryId,
                CreatedBy = "Admin",
                CreatedDate = DateTime.Now,
            };

            transaction = await _repository.AddTransaction(transaction);

            var addedTransaction = _mapper.Map<TransactionDTO>(transaction);

            _response.Data = addedTransaction;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Write")]
    [ResponseCache(CacheProfileName = "DefaultGet")]
    public async Task<IActionResult> GetTransactions(CancellationToken cancellationToken)
    {
        try
        {
            var results = await _repository.GetTransactions(cancellationToken);
            var categories = await _categoryRepository.GetCategories(cancellationToken);

            if (results is null || categories is null)
            {
                _response.Data = null;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            var transactions = MapTransactions(results, categories);

            _response.Data = transactions;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (OperationCanceledException)
        {
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Write")]
    [ResponseCache(CacheProfileName = "DefaultGet")]
    public async Task<IActionResult> GetTransaction(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetTransaction(id, cancellationToken);

            if (result is null) return NotFound();

            var transaction = _mapper.Map<TransactionDTO>(result);

            _response.Data = transaction;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (OperationCanceledException)
        {
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Write")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] ExpenseRequest request)
    {
        try
        {              
            var transaction = new Transactions()
            {
                Item = request.Item,
                Amount = request.Amount,
                Comment = request.Comment,
                CategoryId = request.CategoryId,
            };

            transaction = await _repository.UpdateTransaction(id, transaction);

            if (transaction is null) return NotFound();

            var updatedTransaction = _mapper.Map<TransactionDTO>(transaction);

            _response.Data = updatedTransaction;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Write")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        try
        {
            var transaction = await _repository.DeleteTransaction(id);

            if (transaction == null) return NotFound();

            var deletedTransaction = _mapper.Map<TransactionDTO>(transaction);

            _response.Data = deletedTransaction;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    #region Helper Methods

    private IEnumerable<TransactionDTO> MapTransactions(IEnumerable<Transactions> results, IEnumerable<Categories> categories)
    {
        return from transaction in results
               join category in categories
               on transaction.CategoryId equals category.Id
               select new TransactionDTO
               {
                   TransactionId = transaction.Id,
                   Amount = transaction.Amount,
                   Comment = transaction.Comment,
                   Item = transaction.Item,
                   Category = category.Title,
                   CreatedDate = transaction.CreatedDate,
               };
    }

    #endregion Helper Methods
}
