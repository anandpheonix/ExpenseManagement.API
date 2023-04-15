using DataAccess.Models;

namespace DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<Transactions> AddTransaction(Transactions transaction);
    Task<Transactions> DeleteTransaction(int id);
    Task<Transactions> GetTransaction(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Transactions>> GetTransactions(CancellationToken cancellationToken);
    Task<Transactions> UpdateTransaction(int id, Transactions transaction);
}
