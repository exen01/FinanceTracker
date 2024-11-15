using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Services;

public class CategoryService : ICategoryService
{
  private readonly ICategoryRepository _categoryRepository;

  public async Task<Category?> GetCategoryById(int id)
  {
    return await _categoryRepository.GetCategoryById(id);
  }

  public async Task<Category> AddCategory(Category category)
  {
    return await _categoryRepository.AddCategory(category);
  }

  public async Task<bool> DeleteCategoryById(int id)
  {
    return await _categoryRepository.DeleteCategoryById(id);
  }

  public async Task<Category> UpdateCategory(Category category)
  {
    return await _categoryRepository.UpdateCategory(category);
  }

  public async Task<List<Category>> GetAllCategories()
  {
    return await _categoryRepository.GetAllCategories();
  }

  public async Task<List<Category>> GetCategoriesByType(TransactionType type)
  {
    return await _categoryRepository.GetCategoriesByType(type);
  }

  public Task SoftDeleteCategoryById(int id)
  {
    return _categoryRepository.SoftDeleteCategoryById(id);
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