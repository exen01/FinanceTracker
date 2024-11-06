using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;

    [DataType(DataType.Currency)]
    [Display(Name = "Общий доход")]
    public decimal TotalIncome { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Общий расход")]
    public decimal TotalExpense { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Баланс")]
    public decimal TotalBalance { get; set; }

    public List<FinanceTracker.Domain.Entities.Transaction> Transactions { get; set; }
    public List<FinanceTracker.Domain.Entities.Category> Categories { get; set; }
    public Dictionary<string, decimal> CategoryExpenses { get; set; }
    public List<ExpenseOverTime> ExpensesByDate { get; set; }
    public List<ExpenseOverTime> IncomeByDate { get; set; }

    public IndexModel(ITransactionService transactionService, ICategoryService categoryService,
      ILogger<IndexModel> logger)
    {
      _transactionService = transactionService;
      _categoryService = categoryService;
      _logger = logger;
    }

    public async Task OnGetAsync()
    {
      TotalIncome = await _transactionService.GetTotalIncome();
      TotalExpense = await _transactionService.GetTotalExpense();
      TotalBalance = await _transactionService.GetTotalBalance();

      Transactions = await _transactionService.GetAllTransactions();
      Categories = await _categoryService.GetCategoriesByType(TransactionType.Expense);

      CategoryExpenses = Categories.ToDictionary(
        category => category.CategoryName,
        category => Transactions
          .Where(t => t.Category.Id == category.Id)
          .Sum(t => t.Amount));

      ExpensesByDate = Transactions
        .Where(t => t.TransactionType == TransactionType.Expense)
        .GroupBy(t => t.Date.Date)
        .Select(g => new ExpenseOverTime
        {
          Date = g.Key,
          TotalAmount = g.Sum(t => t.Amount)
        })
        .OrderBy(e => e.Date)
        .ToList();

      IncomeByDate = Transactions
        .Where(t => t.TransactionType == TransactionType.Income)
        .GroupBy(t => t.Date.Date)
        .Select(g => new ExpenseOverTime
        {
          Date = g.Key,
          TotalAmount = g.Sum(t => t.Amount)
        })
        .OrderBy(e => e.Date)
        .ToList();
    }
  }
}