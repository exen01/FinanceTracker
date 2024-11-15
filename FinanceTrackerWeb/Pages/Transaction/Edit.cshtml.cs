using FinanceTracker.Domain.Abstractions;
using FinanceTrackerWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTrackerWeb.Pages.Transaction;

public class EditModel : PageModel
{
  private readonly ITransactionService _transactionService;
  private readonly ICategoryService _categoryService;

  public EditModel(ITransactionService transactionService, ICategoryService categoryService)
  {
    _transactionService = transactionService;
    _categoryService = categoryService;
  }

  [BindProperty] public TransactionDto TransactionDto { get; set; } = default!;
  public List<SelectListItem> Categories { get; set; }
  public string? ErrorMessage { get; set; }

  public async Task<IActionResult> OnGetAsync(Guid? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var transaction = await _transactionService.GetTransactionById(id.Value);
    if (transaction == null)
    {
      return NotFound();
    }

    TransactionDto = new TransactionDto
    {
      Amount = transaction.Amount,
      Date = transaction.Date,
      Description = transaction.Description,
      CategoryId = transaction.Category.Id,
      TransactionType = transaction.TransactionType
    };

    var categoriesList = await _categoryService.GetAllCategories();
    Categories = categoriesList
      .Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = $"{c.CategoryName} ({c.TransactionType})"
      }).ToList();

    return Page();
  }

  public async Task<IActionResult> OnPostAsync(Guid? id)
  {
    if (!ModelState.IsValid || id == null)
    {
      return Page();
    }

    var transactionCategory = await _categoryService.GetCategoryById(TransactionDto.CategoryId);
    if (transactionCategory == null) return Page();

    var updatedTransaction = new FinanceTracker.Domain.Entities.Transaction
    {
      Id = id.Value,
      Amount = TransactionDto.Amount,
      Date = TransactionDto.Date,
      Description = TransactionDto.Description ?? string.Empty,
      TransactionType = transactionCategory.TransactionType,
      Category = transactionCategory
    };

    try
    {
      await _transactionService.UpdateTransaction(updatedTransaction);
    }
    catch (Exception e)
    {
      ErrorMessage = e.Message;
      await OnGetAsync(id);
      return Page();
    }

    return RedirectToPage("./Index");
  }
}