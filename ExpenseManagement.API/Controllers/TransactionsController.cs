using Application.Controllers.Interfaces;
using AutoMapper;
using Common;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Objects;
using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase, ITransactionsController
    {
        protected APIResponse _response;
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _response = new();
        }

        [HttpPost]
        [ActionName("transaction/add")]
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
                    CreatedBy = "Pheonix"
                };

                transaction = await _repository.AddTransaction(transaction);

                var addedTransaction = _mapper.Map<TransactionDTO>(transaction);

                _response.Data = addedTransaction;
                _response.StatusCode = HttpStatusCode.OK;

                return CreatedAtAction(nameof(GetTransaction), new { id = addedTransaction.TransactionId }, _response);
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                throw;
            }
        }

        [HttpGet]
        [ActionName("transactions")]
        [ResponseCache(CacheProfileName = "DefaultGet")]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var results = await _repository.GetTransactions();

                if (results is null)
                {
                    _response.Data = null;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                var transactions = _mapper.Map<List<TransactionDTO>>(results);

                _response.Data = transactions;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [ActionName("transaction")]
        [ResponseCache(CacheProfileName = "DefaultGet")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            try
            {
                var result = await _repository.GetTransaction(id);

                if (result is null) return NotFound();

                var transaction = _mapper.Map<TransactionDTO>(result);

                _response.Data = transaction;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [ActionName("transaction/update")]
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
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [ActionName("transaction/delete")]
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
