﻿using DataAccess.DBContext;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ExpensesContext _dbContext;

    public TransactionRepository(ExpensesContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Transactions> AddTransaction(Transactions transaction)
    {
        await _dbContext.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();

        return transaction;
    }

    public async Task<IEnumerable<Transactions>> GetTransactions(CancellationToken cancellationToken)
    {
        return await _dbContext.Transactions.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Transactions> GetTransaction(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Transactions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Transactions> UpdateTransaction(int id, Transactions transaction)
    {
        var existingtransaction = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);

        if (existingtransaction is null)
        {
            return null;
        }

        existingtransaction.Item = transaction.Item;
        existingtransaction.Amount = transaction.Amount;
        existingtransaction.Comment = transaction.Comment;
        existingtransaction.CategoryId = transaction.CategoryId;

        await _dbContext.SaveChangesAsync();

        return existingtransaction;
    }

    public async Task<Transactions> DeleteTransaction(int id)
    {
        var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);

        if (transaction is null)
        {
            return null;
        }

        _dbContext.Transactions.Remove(transaction);
        await _dbContext.SaveChangesAsync();

        return transaction;
    }
}
