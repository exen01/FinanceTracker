using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Transaction;

public class TransactionRepository : ITransactionRepository
{
  private readonly ApplicationDbContext _dbContext;
  private readonly DbSet<Domain.Entities.Transaction> _transactionsDbSet;

  public Domain.Entities.Transaction AddTransaction(Domain.Entities.Transaction transaction)
  {
    var savedTransaction = _transactionsDbSet.Add(transaction);
    _dbContext.SaveChanges();

    return savedTransaction.Entity;
  }

  public Domain.Entities.Transaction UpdateTransaction(Domain.Entities.Transaction transaction)
  {
    var updatedTransaction = _transactionsDbSet.Update(transaction);
    _dbContext.SaveChanges();

    return updatedTransaction.Entity;
  }

  public bool DeleteTransaction(Guid transactionId)
  {
    var transaction = _transactionsDbSet.Find(transactionId);

    if (transaction == null)
    {
      return false;
    }

    _transactionsDbSet.Remove(transaction);
    _dbContext.SaveChanges();

    return true;
  }

  public Domain.Entities.Transaction? GetTransactionById(Guid transactionId)
  {
    return _transactionsDbSet.Find(transactionId);
  }

  public decimal GetTotalBalance()
  {
    decimal totalBalance = 0;
    foreach (var transaction in _transactionsDbSet.ToList())
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

  public List<Domain.Entities.Transaction> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
  {
    return _transactionsDbSet.Where(t => t.Date >= startDate && t.Date <= endDate).ToList();
  }

  public List<Domain.Entities.Transaction> GetTransactionsByDate(DateTime date)
  {
    return _transactionsDbSet.Where(t => t.Date == date).ToList();
  }

  public List<Domain.Entities.Transaction> GetTransactionsByCategory(Domain.Entities.Category category)
  {
    return _transactionsDbSet.Where(t => t.Category == category).ToList();
  }

  public List<Domain.Entities.Transaction> GetTransactionsByType(TransactionType transactionType)
  {
    return _transactionsDbSet.Where(t => t.TransactionType == transactionType).ToList();
  }

  public List<Domain.Entities.Transaction> GetAllTransactions()
  {
    return _transactionsDbSet.Include(t => t.Category).ToList();
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