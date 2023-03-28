using DataTransfer.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.IControllers
{
    public interface ITransactionsController
    {
        Task<IActionResult> AddTransaction([FromBody] ExpenseRequest request);
        Task<IActionResult> GetTransactions(CancellationToken cancellationToken);
        Task<IActionResult> GetTransaction(int id, CancellationToken cancellationToken);
        Task<IActionResult> UpdateTransaction(int id, [FromBody] ExpenseRequest request);
        Task<IActionResult> DeleteTransaction(int id);
    }
}
