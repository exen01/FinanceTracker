using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

public class Category
{
  public int Id { get; set; }
  public string CategoryName { get; set; }
  public TransactionType TransactionType { get; set; }
}