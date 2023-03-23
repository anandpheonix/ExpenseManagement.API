using DataAccess.Models;

namespace DataAccess.Repositories;

public interface ITransactionRepository
{
    Task<Transactions> AddTransaction(Transactions transaction);
    Task<Transactions> DeleteTransaction(int id);
    Task<Transactions> GetTransaction(int id);
    Task<IEnumerable<Transactions>> GetTransactions();
    Task<Transactions> UpdateTransaction(int id, Transactions transaction);
}
