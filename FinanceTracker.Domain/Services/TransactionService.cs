using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Services;

public class TransactionService : ITransactionService
{
  private readonly ITransactionRepository _transactionRepository;

  public async Task<Transaction> AddTransaction(Transaction transaction)
  {
    ValidateTransactionAmount(transaction);
    ValidateTransactionCategory(transaction);

    if (transaction.TransactionType == TransactionType.Expense)
    {
      var currentBalance = await GetTotalBalance();
      if (currentBalance - transaction.Amount < 0)
      {
        throw new InvalidOperationException("Недостаточный баланс для транзакции.");
      }
    }

    return await _transactionRepository.AddTransaction(transaction);
  }

  public async Task<Transaction> UpdateTransaction(Transaction transaction)
  {
    ValidateTransactionAmount(transaction);
    ValidateTransactionCategory(transaction);

    return await _transactionRepository.UpdateTransaction(transaction);
  }

  public async Task<bool> DeleteTransactionById(Guid transactionId)
  {
    return await _transactionRepository.DeleteTransaction(transactionId);
  }

  public async Task<Transaction?> GetTransactionById(Guid transactionId)
  {
    return await _transactionRepository.GetTransactionById(transactionId);
  }

  public async Task<decimal> GetTotalBalance()
  {
    return await _transactionRepository.GetTotalBalance();
  }

  public async Task<decimal> GetTotalIncome()
  {
    var incomeTransactions = await _transactionRepository.GetTransactionsByType(TransactionType.Income);

    return incomeTransactions.Sum(incomeTransaction => incomeTransaction.Amount);
  }

  public async Task<decimal> GetTotalExpense()
  {
    var expenseTransactions = await _transactionRepository.GetTransactionsByType(TransactionType.Expense);

    return expenseTransactions.Sum(expenseTransaction => expenseTransaction.Amount);
  }

  public decimal GetBalanceForTransactions(IList<Transaction> transactions)
  {
    var balance = decimal.Zero;

    foreach (var transaction in transactions)
    {
      switch (transaction.TransactionType)
      {
        case TransactionType.Income:
          balance += transaction.Amount;
          break;
        case TransactionType.Expense:
          balance -= transaction.Amount;
          break;
      }
    }

    return balance;
  }

  public async Task<List<Transaction>> GetTransactionsByDateRange(DateTime startDate, DateTime endDate)
  {
    var transactions = await _transactionRepository.GetTransactionsByDateRange(startDate, endDate);
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public async Task<List<Transaction>> GetTransactionsByDate(DateTime date)
  {
    var transactions = await _transactionRepository.GetTransactionsByDate(date);
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public async Task<List<Transaction>> GetTransactionsByCategory(Category category)
  {
    var transactions = await _transactionRepository.GetTransactionsByCategory(category);
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public async Task<List<Transaction>> GetTransactionsByCategoryId(int categoryId)
  {
    var transactions = await _transactionRepository.GetTransactionsByCategoryId(categoryId);
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public async Task<List<Transaction>> GetTransactionsByType(TransactionType transactionType)
  {
    var transactions = await _transactionRepository.GetTransactionsByType(transactionType);
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public async Task<List<Transaction>> GetAllTransactions()
  {
    var transactions = await _transactionRepository.GetAllTransactions();
    return transactions.OrderBy(t => t.Date).ToList();
  }

  public Task ImportTransactions(List<Transaction> transactions)
  {
    return _transactionRepository.ImportTransactions(transactions);
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
  /// <param name="categoryRepository">Репозиторий категорий</param>
  public TransactionService(ITransactionRepository transactionRepository)
  {
    _transactionRepository = transactionRepository;
  }
}