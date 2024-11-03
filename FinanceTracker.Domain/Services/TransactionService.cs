using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Services;

public class TransactionService : ITransactionService
{
  private readonly ITransactionRepository _transactionRepository;

  public Transaction AddTransaction(Transaction transaction)
  {
    ValidateTransactionAmount(transaction);
    ValidateTransactionCategory(transaction);

    if (transaction.TransactionType == TransactionType.Expense)
    {
      var currentBalance = GetTotalBalance();
      if (currentBalance - transaction.Amount < 0)
      {
        throw new InvalidOperationException("Недостаточный баланс для транзакции.");
      }
    }

    return _transactionRepository.AddTransaction(transaction);
  }

  public Transaction UpdateTransaction(Transaction transaction)
  {
    ValidateTransactionAmount(transaction);
    ValidateTransactionCategory(transaction);

    return _transactionRepository.UpdateTransaction(transaction);
  }

  public bool DeleteTransactionById(Guid transactionId)
  {
    return _transactionRepository.DeleteTransaction(transactionId);
  }

  public Transaction? GetTransactionById(Guid transactionId)
  {
    return _transactionRepository.GetTransactionById(transactionId);
  }

  public decimal GetTotalBalance()
  {
    return _transactionRepository.GetTotalBalance();
  }

  public List<Transaction> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
  {
    return _transactionRepository.GetTransactionsByDateRange(startDate, endDate).OrderBy(t => t.Date).ToList();
  }

  public List<Transaction> GetTransactionsByDate(DateTime date)
  {
    return _transactionRepository.GetTransactionsByDate(date).OrderBy(t => t.Date).ToList();
  }

  public List<Transaction> GetTransactionsByCategory(Category category)
  {
    return _transactionRepository.GetTransactionsByCategory(category).OrderBy(t => t.Date).ToList();
  }

  public List<Transaction> GetTransactionsByCategoryId(int categoryId)
  {
    return _transactionRepository.GetTransactionsByCategoryId(categoryId).OrderBy(t => t.Date).ToList();
  }

  public List<Transaction> GetTransactionsByType(TransactionType transactionType)
  {
    return _transactionRepository.GetTransactionsByType(transactionType).OrderBy(t => t.Date).ToList();
  }

  public List<Transaction> GetAllTransactions()
  {
    return _transactionRepository.GetAllTransactions().OrderBy(t => t.Date).ToList();
  }

  private void ValidateTransactionAmount(Transaction transaction)
  {
    if (transaction.Amount < 0)
    {
      throw new ArgumentException("Сумма транзакции должна быть положительной.");
    }
  }

  private void ValidateTransactionCategory(Transaction transaction)
  {
    if (transaction.Category == null || transaction.Category.TransactionType != transaction.TransactionType)
    {
      throw new ArgumentException("Недопустимая или несоответствующая категория для транзакции.");
    }
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="transactionRepository">Репозиторий транзакций</param>
  public TransactionService(ITransactionRepository transactionRepository)
  {
    _transactionRepository = transactionRepository;
  }
}