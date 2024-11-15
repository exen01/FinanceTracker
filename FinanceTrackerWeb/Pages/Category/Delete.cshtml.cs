using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Category
{
  public class DeleteModel : PageModel
  {
    private readonly ICategoryService _service;

    public DeleteModel(ICategoryService service)
    {
      _service = service;
    }

    [BindProperty] public FinanceTracker.Domain.Entities.Category Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _service.GetCategoryById(id.Value);

      if (category == null)
      {
        return NotFound();
      }

      Category = category;

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _service.GetCategoryById(id.Value);
      if (category == null) return RedirectToPage("./Index");

      Category = category;
      await _service.SoftDeleteCategoryById(Category.Id);

      return RedirectToPage("./Index");
    }
  }
}