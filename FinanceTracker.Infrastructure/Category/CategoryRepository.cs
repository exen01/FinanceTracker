using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Category;

public class CategoryRepository : ICategoryRepository
{
  private readonly ApplicationDbContext _dbContext;
  private readonly DbSet<Domain.Entities.Category> _categoriesDbSet;

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
    return await _categoriesDbSet.ToListAsync();
  }

  public async Task<Domain.Entities.Category?> GetCategoryById(int categoryId)
  {
    return await _categoriesDbSet.FindAsync(categoryId);
  }

  public async Task<List<Domain.Entities.Category>> GetCategoriesByType(TransactionType type)
  {
    return await _categoriesDbSet.Where(c => c.TransactionType == type).ToListAsync();
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="dbContext">Контекст базы данных</param>
  public CategoryRepository(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext;
    _categoriesDbSet = _dbContext.Set<Domain.Entities.Category>();
  }
}