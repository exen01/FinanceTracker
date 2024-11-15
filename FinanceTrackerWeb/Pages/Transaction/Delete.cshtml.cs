using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Transaction;

public class DeleteModel : PageModel
{
  private readonly ITransactionService _transactionService;

  public DeleteModel(ITransactionService transactionService)
  {
    _transactionService = transactionService;
  }

  [BindProperty] public FinanceTracker.Domain.Entities.Transaction Transaction { get; set; } = default!;

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

    Transaction = transaction;

    return Page();
  }

  public async Task<IActionResult> OnPostAsync(Guid? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var transaction = await _transactionService.GetTransactionById(id.Value);
    if (transaction == null)
    {
      return RedirectToPage("./Index");
    }

    Transaction = transaction;
    await _transactionService.SoftDeleteTransactionById(Transaction.Id);
    return RedirectToPage("./Index");
  }
}