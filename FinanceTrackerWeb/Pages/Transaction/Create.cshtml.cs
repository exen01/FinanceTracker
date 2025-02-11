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
  public string? ErrorMessage { get; set; }

  public CreateModel(ITransactionService transactionService, ICategoryService categoryService)
  {
    _transactionService = transactionService;
    _categoryService = categoryService;
  }

  public async Task OnGetAsync()
  {
    var categoriesList = await _categoryService.GetAllCategories();
    Categories = categoriesList
      .Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = $"{c.CategoryName} ({c.TransactionType})"
      }).ToList();
  }

  public async Task<IActionResult> OnPostAsync()
  {
    if (!ModelState.IsValid)
    {
      await OnGetAsync();
      return Page();
    }

    var transactionCategory = await _categoryService.GetCategoryById(TransactionDto.CategoryId);

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

    try
    {
      await _transactionService.AddTransaction(newTransaction);
    }
    catch (InvalidOperationException e)
    {
      ErrorMessage = e.Message;
      return Page();
    }

    return RedirectToPage("./Index");
  }
}