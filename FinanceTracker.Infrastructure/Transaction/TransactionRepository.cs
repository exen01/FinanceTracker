using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Transaction;

public class TransactionRepository : ITransactionRepository
{
  private readonly ApplicationDbContext _dbContext;
  private readonly DbSet<Domain.Entities.Transaction> _transactionsDbSet;

  public async Task<Domain.Entities.Transaction> AddTransaction(Domain.Entities.Transaction transaction)
  {
    var savedTransaction = await _transactionsDbSet.AddAsync(transaction);
    await _dbContext.SaveChangesAsync();

    return savedTransaction.Entity;
  }

  public async Task<Domain.Entities.Transaction> UpdateTransaction(Domain.Entities.Transaction transaction)
  {
    var updatedTransaction = _transactionsDbSet.Update(transaction);
    await _dbContext.SaveChangesAsync();

    return updatedTransaction.Entity;
  }

  public async Task<bool> DeleteTransaction(Guid transactionId)
  {
    var transaction = await _transactionsDbSet.FindAsync(transactionId);

    if (transaction == null)
    {
      return false;
    }

    _transactionsDbSet.Remove(transaction);
    await _dbContext.SaveChangesAsync();

    return true;
  }

  public async Task<Domain.Entities.Transaction?> GetTransactionById(Guid transactionId)
  {
    return await _transactionsDbSet.FindAsync(transactionId);
  }

  public async Task<decimal> GetTotalBalance()
  {
    decimal totalBalance = 0;
    foreach (var transaction in await _transactionsDbSet.ToListAsync())
    {
      switch (transaction.TransactionType)
      {
        case TransactionType.Income:
          totalBalance += transaction.Amount;
          break;
        case TransactionType.Expense:
          totalBalance -= transaction.Amount;
          break;
      }
    }

    return totalBalance;
  }

  public async Task<List<Domain.Entities.Transaction>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
  {
    return await _transactionsDbSet
      .Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date)
      .ToListAsync();
  }

  public async Task<List<Domain.Entities.Transaction>> GetTransactionsByDate(DateTime date)
  {
    return await _transactionsDbSet
      .Where(t => t.Date.Date == date.Date)
      .ToListAsync();
  }

  public async Task<List<Domain.Entities.Transaction>> GetTransactionsByCategory(Domain.Entities.Category category)
  {
    return await _transactionsDbSet
      .Where(t => t.Category == category)
      .ToListAsync();
  }

  public async Task<List<Domain.Entities.Transaction>> GetTransactionsByCategoryId(int categoryId)
  {
    return await _transactionsDbSet
      .Where(t => t.Category.Id == categoryId)
      .ToListAsync();
  }

  public async Task<List<Domain.Entities.Transaction>> GetTransactionsByType(TransactionType transactionType)
  {
    return await _transactionsDbSet
      .Where(t => t.TransactionType == transactionType)
      .ToListAsync();
  }

  public async Task<List<Domain.Entities.Transaction>> GetAllTransactions()
  {
    return await _transactionsDbSet
      .Include(t => t.Category)
      .ToListAsync();
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="dbContext">Контекст базы данных</param>
  public TransactionRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
    _transactionsDbSet = _dbContext.Set<Domain.Entities.Transaction>();
  }
}