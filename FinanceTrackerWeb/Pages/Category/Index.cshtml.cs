using FinanceTracker.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Category
{
  public class IndexModel : PageModel
  {
    private readonly ICategoryService _service;

    public IndexModel(ICategoryService service)
    {
      _service = service;
    }

    public IList<FinanceTracker.Domain.Entities.Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
      Category = await _service.GetAllCategories();
    }
  }
}