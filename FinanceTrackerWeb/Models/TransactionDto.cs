using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerWeb.Models;

public record TransactionDto
{
  [Required(ErrorMessage = "Введите сумму транзакции.")]
  [Display(Name = "Сумма транзакции")]
  [DataType(DataType.Currency)]
  [Range(0.01, double.MaxValue, ErrorMessage = "Введите корректную сумму.")]
  public decimal Amount { get; init; }

  [Required(ErrorMessage = "Выберите категорию.")]
  [Display(Name = "Категория")]
  public int CategoryId { get; init; }

  [MaxLength(500, ErrorMessage = "Описание не может превышать 500 символов.")]
  [Display(Name = "Описание")]
  public string? Description { get; init; }

  [Required(ErrorMessage = "Выберите дату транзакции.")]
  [DataType(DataType.DateTime)]
  [Display(Name = "Дата")]
  [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
  public DateTime Date { get; init; }

  [Required(ErrorMessage = "Выберите тип транзакции.")]
  [Display(Name = "Тип транзакции")]
  public TransactionType TransactionType { get; init; }
}