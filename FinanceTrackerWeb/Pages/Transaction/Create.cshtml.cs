using FinanceTracker.Domain.Abstractions;
using FinanceTrackerWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTrackerWeb.Pages.Transaction;

public class CreateModel : PageModel
{
  private readonly ITransactionService _transactionService;
  private readonly ICategoryService _categoryService;

  [BindProperty] public TransactionDto TransactionDto { get; set; } = default!;
  public List<SelectListItem> Categories { get; set; }

  public CreateModel(ITransactionService transactionService, ICategoryService categoryService)
  {
    _transactionService = transactionService;
    _categoryService = categoryService;
  }

  public void OnGet()
  {
    Categories = _categoryService.GetAllCategories()
      .Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = $"{c.CategoryName} ({c.TransactionType})"
      }).ToList();
  }

  public IActionResult OnPost()
  {
    if (!ModelState.IsValid)
    {
      OnGet();
      return Page();
    }

    var transactionCategory = _categoryService.GetCategoryById(TransactionDto.CategoryId);

    if (transactionCategory == null) return Page();

    var newTransaction = new FinanceTracker.Domain.Entities.Transaction
    {
      Id = Guid.NewGuid(),
      Amount = TransactionDto.Amount,
      Category = transactionCategory,
      Date = TransactionDto.Date,
      Description = TransactionDto.Description ?? string.Empty,
      TransactionType = transactionCategory.TransactionType
    };

    _transactionService.AddTransaction(newTransaction);

    return RedirectToPage("./Index");
  }
}