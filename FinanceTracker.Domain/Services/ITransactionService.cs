using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Services;

public interface ITransactionService
{
  Transaction AddTransaction(Transaction transaction);
  Transaction UpdateTransaction(Transaction transaction);
  bool DeleteTransactionById(Guid transactionId);
  Transaction GetTransactionById(Guid transactionId);
  decimal GetTotalBalance();
  List<Transaction> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);
  List<Transaction> GetTransactionsByDate(DateTime date);
  List<Transaction> GetTransactionsByCategory(Category category);
  List<Transaction> GetTransactionsByType(TransactionType transactionType);
}