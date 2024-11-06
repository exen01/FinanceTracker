using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerWeb.Models.Dto;

public record CategoryDto
{
  [Required(ErrorMessage = "Введите название категории.")]
  [MaxLength(100)]
  [Display(Name = "Название категории")]
  public string CategoryName { get; init; }

  [Required(ErrorMessage = "Выберите тип категории")]
  [Display(Name = "Тип категории")]
  public TransactionType TransactionType { get; init; }
}