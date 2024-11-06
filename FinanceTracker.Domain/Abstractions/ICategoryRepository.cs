using FinanceTracker.Domain.Entities;
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
  Task<Category> AddCategory(Category category);

  /// <summary>
  /// Удаляет категорию.
  /// </summary>
  /// <param name="categoryId"></param>
  /// <returns>true, если категория удалена, иначе false</returns>
  Task<bool> DeleteCategoryById(int categoryId);

  /// <summary>
  /// Обновляет категорию.
  /// </summary>
  /// <param name="category">Категория с обновленными данными</param>
  /// <returns>Обновленная категория</returns>
  Task<Category> UpdateCategory(Category category);

  /// <summary>
  /// Получает список всех категорий.
  /// </summary>
  /// <returns>Список категорий</returns>
  Task<List<Category>> GetAllCategories();

  /// <summary>
  /// Получает категорию по идентификатору.
  /// </summary>
  /// <param name="categoryId">Идентификатор категории</param>
  /// <returns>Найденная категория или null</returns>
  Task<Category?> GetCategoryById(int categoryId);

  /// <summary>
  /// Получает список категорий по типу <see cref="TransactionType"/>
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  Task<List<Category>> GetCategoriesByType(TransactionType type);
}