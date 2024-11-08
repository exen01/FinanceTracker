using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Transaction;

public class Settings : PageModel
{
  private readonly ITransactionService _transactionService;

  public Settings(ITransactionService transactionService)
  {
    _transactionService = transactionService;
  }

  [Display(Name = "JSON файл")]
  [Required(ErrorMessage = "Выберите файл для импорта")]
  [FileExtensions(Extensions = "json")]
  [BindProperty]
  public IFormFile? UploadFile { get; set; }

  public async Task<IActionResult> OnPostExport()
  {
    var transactions = await _transactionService.GetAllTransactions();
    var json = JsonSerializer.Serialize(transactions);

    return File(Encoding.UTF8.GetBytes(json), "application/json", "Transactions.json");
  }

  public async Task<IActionResult> OnPostImport()
  {
    if (UploadFile == null || UploadFile.Length == 0)
    {
      ModelState.AddModelError("UploadFile", "Файл не выбран или он пустой.");
      return Page();
    }

    try
    {
      using var stream = new StreamReader(UploadFile.OpenReadStream());
      var json = await stream.ReadToEndAsync();
      var transactions = JsonSerializer.Deserialize<List<FinanceTracker.Domain.Entities.Transaction>>(json);

      if (transactions != null)
      {
        await _transactionService.ImportTransactions(transactions);
      }
    }
    catch (JsonException e)
    {
      ModelState.AddModelError("", $"Ошибка при чтении JSON: {e.Message}");
      return Page();
    }

    return RedirectToPage();
  }

  public void OnGet()
  {
  }
}