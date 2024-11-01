using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Transaction;

public class IndexModel : PageModel
{
  private readonly ITransactionService _transactionService;

  public IndexModel(ITransactionService transactionService)
  {
    _transactionService = transactionService;
  }
  
  public List<FinanceTracker.Domain.Entities.Transaction> Transactions { get; set; }
  public decimal Balance { get; set; }

  public IActionResult OnGet()
  {
    Transactions = _transactionService.GetAllTransactions();
    Balance = _transactionService.GetTotalBalance();

    return Page();
  }
}