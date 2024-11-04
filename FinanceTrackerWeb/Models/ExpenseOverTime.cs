namespace FinanceTrackerWeb.Models;

public record ExpenseOverTime
{
  public DateTime Date { get; init; }
  public decimal TotalAmount { get; init; }
}