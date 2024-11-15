using System.ComponentModel.DataAnnotations;
using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Enums;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly ITransactionService _transactionService;
    private readonly ICategoryService _categoryService;

    public IndexModel(ITransactionService transactionService, ICategoryService categoryService,
      ILogger<IndexModel> logger)
    {
      _transactionService = transactionService;
      _categoryService = categoryService;
      _logger = logger;
    }

    [DataType(DataType.Currency)]
    [Display(Name = "Доход за месяц")]
    public decimal TotalIncome { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Расход за месяц")]
    public decimal TotalExpense { get; set; }

    [DataType(DataType.Currency)]
    [Display(Name = "Текущий баланс")]
    public decimal TotalBalance { get; set; }

    [BindProperty(SupportsGet = true)]
    [Display(Name = "Месяц")]
    public DateTime SelectedMonth { get; set; }

    public List<FinanceTracker.Domain.Entities.Transaction> Transactions { get; set; }
    public List<FinanceTracker.Domain.Entities.Category> Categories { get; set; }
    public Dictionary<string, decimal> CategoryExpenses { get; set; }
    public List<ExpenseOverTime> ExpensesByDate { get; set; }
    public List<ExpenseOverTime> IncomeByDate { get; set; }

    public async Task OnGetAsync()
    {
      if (SelectedMonth == DateTime.MinValue)
      {
        SelectedMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
      }

      var startDate = new DateTime(SelectedMonth.Year, SelectedMonth.Month, 1);
      var endDate = startDate.AddMonths(1).AddDays(-1);

      Transactions = await _transactionService.GetTransactionsByDateRange(startDate, endDate);

      TotalIncome = Transactions
        .Where(t => t.TransactionType == TransactionType.Income)
        .Sum(t => t.Amount);

      TotalExpense = Transactions
        .Where(t => t.TransactionType == TransactionType.Expense)
        .Sum(t => t.Amount);

      TotalBalance = await _transactionService.GetTotalBalance();

      Categories = await _categoryService.GetCategoriesByType(TransactionType.Expense);

      var categoryExpenses = new Dictionary<string, decimal>();
      foreach (var transaction in Transactions.Where(t => t.TransactionType == TransactionType.Expense))
      {
        if (!categoryExpenses.TryAdd(transaction.Category.CategoryName, transaction.Amount))
        {
          categoryExpenses[transaction.Category.CategoryName] += transaction.Amount;
        }
      }

      CategoryExpenses = categoryExpenses;

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