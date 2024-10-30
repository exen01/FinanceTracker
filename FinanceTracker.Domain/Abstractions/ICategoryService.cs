using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Domain.Abstractions;

public interface ICategoryService
{
  /// <summary>
  /// Получает категорию по идентификатору.
  /// </summary>
  /// <param name="id">Идентификатор категории</param>
  /// <returns>Найденная категория или null</returns>
  Category? GetCategoryById(int id);
  
  /// <summary>
  /// Добавляет новую категорию.
  /// </summary>
  /// <param name="category">Данные новой категории</param>
  /// <returns>Новая категория</returns>
  Category AddCategory(Category category);
  
  /// <summary>
  /// Удаляет категорию по идентификатору.
  /// </summary>
  /// <param name="id">Идентификатор категории</param>
  /// <returns>true, если категория удалена, иначе false</returns>
  bool DeleteCategoryById(int id);
  
  /// <summary>
  /// Обновляет данные категории.
  /// </summary>
  /// <param name="category">Категория с обновленными данными</param>
  /// <returns>Обновленная категория</returns>
  Category UpdateCategory(Category category);
  
  /// <summary>
  /// Получает список всех категорий.
  /// </summary>
  /// <returns>Списое всех категорий</returns>
  List<Category> GetAllCategories();
}