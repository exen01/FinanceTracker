using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Domain.Services;

public class CategoryService : ICategoryService
{
  private readonly ICategoryRepository _categoryRepository;

  public Category? GetCategoryById(int id)
  {
    return _categoryRepository.GetCategoryById(id);
  }

  public Category AddCategory(Category category)
  {
    return _categoryRepository.AddCategory(category);
  }

  public bool DeleteCategoryById(int id)
  {
    return _categoryRepository.DeleteCategoryById(id);
  }

  public Category UpdateCategory(Category category)
  {
    return _categoryRepository.UpdateCategory(category);
  }

  public List<Category> GetAllCategories()
  {
    return _categoryRepository.GetAllCategories();
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="categoryRepository">Репозиторий для категорий</param>
  public CategoryService(ICategoryRepository categoryRepository)
  {
    _categoryRepository = categoryRepository;
  }
}