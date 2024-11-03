﻿using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Category;

public class CategoryRepository : ICategoryRepository
{
  private readonly ApplicationDbContext _dbContext;
  private readonly DbSet<Domain.Entities.Category> _categoriesDbSet;

  public Domain.Entities.Category AddCategory(Domain.Entities.Category category)
  {
    var categoryEntity = _categoriesDbSet.Add(category);
    _dbContext.SaveChanges();

    return categoryEntity.Entity;
  }

  public bool DeleteCategoryById(int categoryId)
  {
    var category = _categoriesDbSet.Find(categoryId);
    if (category == null)
    {
      return false;
    }

    _categoriesDbSet.Remove(category);
    _dbContext.SaveChanges();

    return true;
  }

  public Domain.Entities.Category UpdateCategory(Domain.Entities.Category category)
  {
    var updatedCategory = _categoriesDbSet.Update(category);
    _dbContext.SaveChanges();
    return updatedCategory.Entity;
  }

  public List<Domain.Entities.Category> GetAllCategories()
  {
    return _categoriesDbSet.ToList();
  }

  public Domain.Entities.Category? GetCategoryById(int categoryId)
  {
    return _categoriesDbSet.Find(categoryId);
  }

  public List<Domain.Entities.Category> GetCategoriesByType(TransactionType type)
  {
    return _categoriesDbSet.Where(c => c.TransactionType == type).ToList();
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