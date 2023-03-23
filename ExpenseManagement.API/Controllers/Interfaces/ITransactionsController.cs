using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.Interfaces
{
    public interface ITransactionsController
    {
        Task<IActionResult> AddTransaction([FromBody] ExpenseRequest request);
        Task<IActionResult> GetTransactions();
        Task<IActionResult> GetTransaction(int id);
        Task<IActionResult> UpdateTransaction(int id, [FromBody] ExpenseRequest request);
        Task<IActionResult> DeleteTransaction(int id);
    }
}
