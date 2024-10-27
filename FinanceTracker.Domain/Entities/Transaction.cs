using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public class Transaction
{
  public Guid Id { get; set; }
  public decimal Amount { get; set; }
  public Category Category { get; set; }
  public TransactionType TransactionType { get; set; }
  public DateTime Date { get; set; }
  public string Description { get; set; }
}