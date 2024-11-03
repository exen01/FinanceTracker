using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly ITransactionService _transactionService;

    [DataType(DataType.Currency)]
    [Display(Name = "Общий доход")]
    public decimal TotalIncome { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Общий расход")]
    public decimal TotalExpense { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Баланс")]
    public decimal TotalBalance { get; set; }

    public IndexModel(ITransactionService transactionService, ILogger<IndexModel> logger)
    {
      _transactionService = transactionService;
      _logger = logger;
    }

    public void OnGet()
    {
      TotalIncome = _transactionService.GetTotalIncome();
      TotalExpense = _transactionService.GetTotalExpense();
      TotalBalance = _transactionService.GetTotalBalance();
    }
  }
}