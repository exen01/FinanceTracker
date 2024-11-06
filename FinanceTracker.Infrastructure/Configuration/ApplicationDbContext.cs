using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Configuration;

/// <summary>
/// Контекст базы данных для приложения.
/// </summary>
public class ApplicationDbContext : DbContext
{
  /// <summary>
  /// Таблица транзакций.
  /// </summary>
  public DbSet<Domain.Entities.Transaction> Transactions { get; set; }

  /// <summary>
  /// Таблица категорий.
  /// </summary>
  public DbSet<Domain.Entities.Category> Categories { get; set; }

  /// <summary>
  /// Настраивает сущности при создании модели базы данных.
  /// </summary>
  /// <param name="modelBuilder">Построитель модели для настройки сущностей</param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Domain.Entities.Transaction>(entity =>
    {
      entity.HasKey(e => e.Id);

      entity.Property(e => e.Date)
        .HasColumnType("datetime")
        .IsRequired();

      entity.Property(e => e.Description)
        .HasMaxLength(500);

      entity.Property(e => e.Amount)
        .IsRequired();

      entity.HasOne(e => e.Category)
        .WithMany()
        .HasForeignKey("CategoryId")
        .IsRequired();
    });

    modelBuilder.Entity<Domain.Entities.Category>(entity =>
    {
      entity.HasKey(e => e.Id);

      entity.Property(e => e.CategoryName)
        .HasMaxLength(100)
        .IsRequired();

      entity.Property(e => e.TransactionType)
        .IsRequired();
    });
  }

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="options">Опции контекста</param>
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }
}