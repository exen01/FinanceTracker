namespace FinanceTracker.Infrastructure.Category;

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
  Domain.Entities.Category AddCategory(Domain.Entities.Category category);

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
  Domain.Entities.Category UpdateCategory(Domain.Entities.Category category);

  /// <summary>
  /// Получает список всех категорий.
  /// </summary>
  /// <returns>Список категорий</returns>
  List<Domain.Entities.Category> GetAllCategories();

  /// <summary>
  /// Получает категорию по идентификатору.
  /// </summary>
  /// <param name="categoryId">Идентификатор категории</param>
  /// <returns>Найденная категория или null</returns>
  Domain.Entities.Category? GetCategoryById(int categoryId);
}