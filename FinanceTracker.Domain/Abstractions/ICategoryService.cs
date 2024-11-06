using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Abstractions;

public interface ICategoryService
{
  /// <summary>
  /// Получает категорию по идентификатору.
  /// </summary>
  /// <param name="id">Идентификатор категории</param>
  /// <returns>Найденная категория или null</returns>
  Task<Category?> GetCategoryById(int id);

  /// <summary>
  /// Добавляет новую категорию.
  /// </summary>
  /// <param name="category">Данные новой категории</param>
  /// <returns>Новая категория</returns>
  Task<Category> AddCategory(Category category);

  /// <summary>
  /// Удаляет категорию по идентификатору.
  /// </summary>
  /// <param name="id">Идентификатор категории</param>
  /// <returns>true, если категория удалена, иначе false</returns>
  Task<bool> DeleteCategoryById(int id);

  /// <summary>
  /// Обновляет данные категории.
  /// </summary>
  /// <param name="category">Категория с обновленными данными</param>
  /// <returns>Обновленная категория</returns>
  Task<Category> UpdateCategory(Category category);

  /// <summary>
  /// Получает список всех категорий.
  /// </summary>
  /// <returns>Список всех категорий</returns>
  Task<List<Category>> GetAllCategories();

  /// <summary>
  /// Получает список категорий по типу <see cref="TransactionType"/>.
  /// </summary>
  /// <param name="type">Тип категории</param>
  /// <returns>Список категорий</returns>
  Task<List<Category>> GetCategoriesByType(TransactionType type);
}