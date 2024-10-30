using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Enums;

namespace FinanceTrackerWeb.Models;

public class TransactionViewModel
{
  [Required(ErrorMessage = "Введите сумму транзакции.")]
  [Display(Name = "Сумма транзакции")]
  public decimal Amount { get; set; }

  [Required(ErrorMessage = "Выберите категорию.")]
  [Display(Name = "Категория")]
  public int CategoryId { get; set; }

  [Required(AllowEmptyStrings = true)]
  [MaxLength(500, ErrorMessage = "Описание не может превышать 500 символов.")]
  [Display(Name = "Описание")]
  public string Description { get; set; }

  [Required(ErrorMessage = "Выберите дату транзакции.")]
  [DataType(DataType.DateTime)]
  [Display(Name = "Дата")]
  public DateTime Date { get; set; }

  [Required(ErrorMessage = "Выберите тип транзакции.")]
  [Display(Name = "Тип транзакции")]
  public TransactionType TransactionType { get; set; }
}