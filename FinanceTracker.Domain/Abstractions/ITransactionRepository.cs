using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Abstractions;

/// <summary>
/// Основные операции с транзакциями.
/// </summary>
public interface ITransactionRepository
{
  /// <summary>
  /// Создает новую транзакцию.
  /// </summary>
  /// <param name="transaction">Данные новой транзакции</param>
  /// <returns>Новая транзакция</returns>
  Domain.Entities.Transaction AddTransaction(Domain.Entities.Transaction transaction);

  /// <summary>
  /// Обновляет данные существующей транзакции.
  /// </summary>
  /// <param name="transaction">Транзакция с обновленными данными</param>
  /// <returns>Обновленная транзакция</returns>
  Domain.Entities.Transaction UpdateTransaction(Domain.Entities.Transaction transaction);

  /// <summary>
  /// Удаляет транзакцию по идентификатору.
  /// </summary>
  /// <param name="transactionId">Идентификатор транзакции</param>
  /// <returns>true, если транзакция удалена, иначе false</returns>
  bool DeleteTransaction(Guid transactionId);

  /// <summary>
  /// Получает транзакцию по её идентификатору.
  /// </summary>
  /// <param name="transactionId">Идентификатор транзакции</param>
  /// <returns>Найденная транзакция или null</returns>
  Domain.Entities.Transaction? GetTransactionById(Guid transactionId);

  /// <summary>
  /// Получает текущий баланс на основе всех транзакций.
  /// </summary>
  /// <returns>Текущий баланс</returns>
  decimal GetTotalBalance();

  /// <summary>
  /// Получает список транзакций за период.
  /// </summary>
  /// <param name="startDate">Начало периода</param>
  /// <param name="endDate">Конец периода</param>
  /// <returns>Список транзакций</returns>
  List<Domain.Entities.Transaction> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);

  /// <summary>
  /// Получает список транзакций на конкретную дату.
  /// </summary>
  /// <param name="date">Дата</param>
  /// <returns>Список транзакций</returns>
  List<Domain.Entities.Transaction> GetTransactionsByDate(DateTime date);

  /// <summary>
  /// Получает список транзакций по категории <see cref="Category"/>.
  /// </summary>
  /// <param name="category">Категория</param>
  /// <returns>Список транзакций</returns>
  List<Domain.Entities.Transaction> GetTransactionsByCategory(Domain.Entities.Category category);

  /// <summary>
  /// Получает список транзакций по типу <see cref="TransactionType"/>.
  /// </summary>
  /// <param name="transactionType">Тип</param>
  /// <returns>Список транзакций</returns>
  List<Domain.Entities.Transaction> GetTransactionsByType(TransactionType transactionType);
}