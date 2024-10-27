using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Configuration;

/// <summary>
/// Контекст базы данных для приложения.
/// </summary>
public class ApplicationDbContext : DbContext
{
  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="options">Опции контекста</param>
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
}