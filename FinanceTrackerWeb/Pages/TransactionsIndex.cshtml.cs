using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Entities;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTrackerWeb.Pages;

public class TransactionsIndex : PageModel
{
  private readonly ITransactionService _transactionService;
  private readonly ICategoryService _categoryService;

  public TransactionsIndex(ITransactionService transactionService, ICategoryService categoryService)
  {
    _transactionService = transactionService;
    _categoryService = categoryService;
  }

  [BindProperty] public TransactionViewModel Transaction { get; set; }

  public List<Transaction> Transactions { get; set; }
  public List<SelectListItem> Categories { get; set; }
  public decimal Balance { get; set; }

  public void OnGet()
  {
    Transactions = _transactionService.GetAllTransactions();
    Categories = _categoryService.GetAllCategories()
      .Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.CategoryName
      }).ToList();
    Balance = _transactionService.GetTotalBalance();
  }

  public IActionResult OnPost()
  {
    if (!ModelState.IsValid)
    {
      OnGet();
      return Page();
    }

    var newTransaction = new Transaction
    {
      Id = Guid.NewGuid(),
      Amount = Transaction.Amount,
      Date = Transaction.Date,
      Description = Transaction.Description,
      Category = _categoryService.GetCategoryById(Transaction.CategoryId),
      TransactionType = Transaction.TransactionType
    };

    _transactionService.AddTransaction(newTransaction);

    return RedirectToPage();
  }
}