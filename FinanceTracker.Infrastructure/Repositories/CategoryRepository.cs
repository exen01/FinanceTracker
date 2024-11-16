using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Repositories;

/// <summary>
/// Реализация основных действий с категориями.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
  #region Поля и свойства

  private readonly ApplicationDbContext _dbContext;
  private readonly DbSet<Domain.Entities.Category> _categoriesDbSet;

  #endregion

  #region Методы

  public async Task<Domain.Entities.Category> AddCategory(Domain.Entities.Category category)
  {
    var categoryEntity = await _categoriesDbSet.AddAsync(category);
    await _dbContext.SaveChangesAsync();

    return categoryEntity.Entity;
  }

  public async Task<bool> DeleteCategoryById(int categoryId)
  {
    var category = await _categoriesDbSet.FindAsync(categoryId);
    if (category == null)
    {
      return false;
    }

    _categoriesDbSet.Remove(category);
    await _dbContext.SaveChangesAsync();

    return true;
  }

  public async Task<Domain.Entities.Category> UpdateCategory(Domain.Entities.Category category)
  {
    var updatedCategory = _categoriesDbSet.Update(category);
    await _dbContext.SaveChangesAsync();
    return updatedCategory.Entity;
  }

  public async Task<List<Domain.Entities.Category>> GetAllCategories()
  {
    return await _categoriesDbSet.Where(c => !c.IsDeleted).ToListAsync();
  }

  public async Task<Domain.Entities.Category?> GetCategoryById(int categoryId)
  {
    var category = await _categoriesDbSet.FindAsync(categoryId);
    if (category != null && category.IsDeleted)
    {
      return null;
    }

    return category;
  }

  public async Task<List<Domain.Entities.Category>> GetCategoriesByType(TransactionType type)
  {
    return await _categoriesDbSet
      .Where(c => !c.IsDeleted)
      .Where(c => c.TransactionType == type)
      .ToListAsync();
  }

  public async Task SoftDeleteCategoryById(int categoryId)
  {
    var category = await _categoriesDbSet.FindAsync(categoryId);
    if (category != null)
    {
      category.IsDeleted = true;
      await _dbContext.SaveChangesAsync();
    }
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="dbContext">Контекст базы данных</param>
  public CategoryRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
    _categoriesDbSet = _dbContext.Set<Domain.Entities.Category>();
  }

  #endregion
}