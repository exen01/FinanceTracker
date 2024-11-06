using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Abstractions;

/// <summary>
/// Основные операции с транзакциями.
/// </summary>
public interface ITransactionService
{
  /// <summary>
  /// Создает новую транзакцию.
  /// </summary>
  /// <param name="transaction">Новая транзакция</param>
  /// <returns>Созданная транзакция</returns>
  /// <exception cref="InvalidOperationException">Если недостаточный баланс для транзакции</exception>
  Transaction AddTransaction(Transaction transaction);

  /// <summary>
  /// Обновляет данные существующей транзакции.
  /// </summary>
  /// <param name="transaction">Транзакция с обновленными данными</param>
  /// <returns>Обновленная транзакция</returns>
  Transaction UpdateTransaction(Transaction transaction);

  /// <summary>
  /// Удаляет транзакцию по идентификатору.
  /// </summary>
  /// <param name="transactionId">Идентификатор транзакции</param>
  /// <returns>true, если транзакция удалена, иначе false</returns>
  bool DeleteTransactionById(Guid transactionId);

  /// <summary>
  /// Получает транзакцию по её идентификатору.
  /// </summary>
  /// <param name="transactionId">Идентификатор транзакции</param>
  /// <returns>Найденная транзакция или null</returns>
  Transaction? GetTransactionById(Guid transactionId);

  /// <summary>
  /// Получает текущий баланс на основе всех транзакций.
  /// </summary>
  /// <returns>Текущий баланс</returns>
  decimal GetTotalBalance();

  /// <summary>
  /// Получает общую сумму доходов.
  /// </summary>
  /// <returns></returns>
  decimal GetTotalIncome();

  /// <summary>
  /// Получает общую сумму расходов.
  /// </summary>
  /// <returns></returns>
  decimal GetTotalExpense();

  /// <summary>
  /// Получает баланс по списку транзакций.
  /// </summary>
  /// <param name="transactions">Список транзакций</param>
  /// <returns>Баланс транзакций</returns>
  decimal GetBalanceForTransactions(IList<Transaction> transactions);

  /// <summary>
  /// Получает список транзакций за период.
  /// </summary>
  /// <param name="startDate">Начало периода</param>
  /// <param name="endDate">Конец периода</param>
  /// <returns>Список транзакций</returns>
  List<Transaction> GetTransactionsByDateRange(DateTime startDate, DateTime endDate);

  /// <summary>
  /// Получает список транзакций на конкретную дату.
  /// </summary>
  /// <param name="date">Дата</param>
  /// <returns>Список транзакций</returns>
  List<Transaction> GetTransactionsByDate(DateTime date);

  /// <summary>
  /// Получает список транзакций по категории <see cref="Category"/>.
  /// </summary>
  /// <param name="category">Категория</param>
  /// <returns>Список транзакций</returns>
  List<Transaction> GetTransactionsByCategory(Category category);

  /// <summary>
  /// Получает список транзакций по идентификатору категории.
  /// </summary>
  /// <param name="categoryId">Идентификатор категории</param>
  /// <returns>Список транзакций</returns>
  List<Transaction> GetTransactionsByCategoryId(int categoryId);

  /// <summary>
  /// Получает список транзакций по типу <see cref="TransactionType"/>.
  /// </summary>
  /// <param name="transactionType">Тип</param>
  /// <returns>Список транзакций</returns>
  List<Transaction> GetTransactionsByType(TransactionType transactionType);

  /// <summary>
  /// Получает список всех транзакций.
  /// </summary>
  /// <returns></returns>
  List<Transaction> GetAllTransactions();
}