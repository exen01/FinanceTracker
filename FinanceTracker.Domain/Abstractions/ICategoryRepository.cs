using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Abstractions;

/// <summary>
/// Основные операции с категориями.
/// </summary>
public interface ICategoryRepository
{
  /// <summary>
  /// Добавляет новую категорию.
  /// </summary>
  /// <param name="category">Данные новой категории</param>
  /// <returns>Новая категория</returns>
  Entities.Category AddCategory(Entities.Category category);

  /// <summary>
  /// Удаляет категорию.
  /// </summary>
  /// <param name="categoryId"></param>
  /// <returns>true, если категория удалена, иначе false</returns>
  bool DeleteCategoryById(int categoryId);

  /// <summary>
  /// Обновляет категорию.
  /// </summary>
  /// <param name="category">Категория с обновленными данными</param>
  /// <returns>Обновленная категория</returns>
  Entities.Category UpdateCategory(Entities.Category category);

  /// <summary>
  /// Получает список всех категорий.
  /// </summary>
  /// <returns>Список категорий</returns>
  List<Entities.Category> GetAllCategories();

  /// <summary>
  /// Получает категорию по идентификатору.
  /// </summary>
  /// <param name="categoryId">Идентификатор категории</param>
  /// <returns>Найденная категория или null</returns>
  Entities.Category? GetCategoryById(int categoryId);

  /// <summary>
  /// Получает список категорий по типу <see cref="TransactionType"/>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  List<Entities.Category> GetCategoriesByType(TransactionType type);
}