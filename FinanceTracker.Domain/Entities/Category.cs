using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Entities;

/// <summary>
/// Данные категории транзакции.
/// </summary>
public class Category
{
  #region Поля и свойства

  /// <summary>
  /// Идентификатор категории.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Название категории.
  /// </summary>
  public string CategoryName { get; set; }

  /// <summary>
  /// Тип категории.
  /// </summary>
  public TransactionType TransactionType { get; set; }

  /// <summary>
  /// Отметка удаления.
  /// </summary>
  public bool IsDeleted { get; set; }

  #endregion
}