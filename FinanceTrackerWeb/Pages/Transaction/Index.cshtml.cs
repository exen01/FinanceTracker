using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Transaction;

public class IndexModel : PageModel
{
  private readonly ITransactionService _transactionService;
  private readonly ICategoryService _categoryService;

  public List<FinanceTracker.Domain.Entities.Transaction> Transactions { get; set; }
  public List<FinanceTracker.Domain.Entities.Category> Categories { get; set; }
  public decimal Balance { get; set; }

  [BindProperty(SupportsGet = true)]
  [Display(Name = "Категория")]
  public int? CategoryId { get; set; }

  [BindProperty(SupportsGet = true)]
  [Display(Name = "Начало периода")]
  [DataType(DataType.Date)]
  public DateTime? StartDate { get; set; }

  [BindProperty(SupportsGet = true)]
  [Display(Name = "Конец периода")]
  [DataType(DataType.Date)]
  public DateTime? EndDate { get; set; }

  public IndexModel(ITransactionService transactionService, ICategoryService categoryService)
  {
    _transactionService = transactionService;
    _categoryService = categoryService;
  }

  public IActionResult OnGet()
  {
    Categories = _categoryService.GetAllCategories();

    if (StartDate.HasValue && EndDate.HasValue && CategoryId.HasValue)
    {
      if (EndDate.Value < StartDate.Value)
      {
        ModelState.AddModelError(string.Empty, "Дата конца периода не может быть раньше даты начала периода.");
        Transactions = new List<FinanceTracker.Domain.Entities.Transaction>();
        return Page();
      }

      Transactions = _transactionService.GetTransactionsByDateRange(StartDate.Value, EndDate.Value)
        .Where(t => t.Category.Id == CategoryId)
        .ToList();
    }
    else if (StartDate.HasValue && EndDate.HasValue)
    {
      if (EndDate.Value < StartDate.Value)
      {
        ModelState.AddModelError(string.Empty, "Дата конца периода не может быть раньше даты начала периода.");
        Transactions = new List<FinanceTracker.Domain.Entities.Transaction>();
        return Page();
      }

      Transactions = _transactionService.GetTransactionsByDateRange(StartDate.Value, EndDate.Value);
    }
    else if (StartDate.HasValue)
    {
      Transactions = _transactionService.GetTransactionsByDate(StartDate.Value);
    }
    else if (CategoryId.HasValue)
    {
      Transactions = _transactionService.GetTransactionsByCategoryId(CategoryId.Value);
    }
    else
    {
      Transactions = _transactionService.GetAllTransactions();
    }

    Balance = _transactionService.GetTotalBalance();

    return Page();
  }
}