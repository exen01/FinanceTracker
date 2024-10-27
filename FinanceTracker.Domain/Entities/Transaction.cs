using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Данные транзакции.
/// </summary>
public class Transaction
{
  /// <summary>
  /// Идентификатор транзакции.
  /// </summary>
  public Guid Id { get; set; }
  
  /// <summary>
  /// Сумма транзакции.
  /// </summary>
  public decimal Amount { get; set; }
  
  /// <summary>
  /// Категория транзакции.
  /// </summary>
  public Category Category { get; set; }
  
  /// <summary>
  /// Тип транзакции.
  /// </summary>
  public TransactionType TransactionType { get; set; }
  
  /// <summary>
  /// Дата транзакции.
  /// </summary>
  public DateTime Date { get; set; }
  
  /// <summary>
  /// Описание транзакции.
  /// </summary>
  public string Description { get; set; }
}