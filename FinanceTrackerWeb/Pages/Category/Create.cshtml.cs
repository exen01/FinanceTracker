using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Infrastructure.Configuration;
using FinanceTrackerWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Category
{
  public class CreateModel : PageModel
  {
    private readonly ICategoryService _categoryService;

    public CreateModel(ApplicationDbContext context, ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    public IActionResult OnGet()
    {
      return Page();
    }

    [BindProperty] public CategoryDto CategoryDto { get; set; } = default!;

    public IActionResult OnPostAsync()
    {
      if (!ModelState.IsValid)
      {
        return Page();
      }

      var newCategory = new FinanceTracker.Domain.Entities.Category
      {
        CategoryName = CategoryDto.CategoryName,
        TransactionType = CategoryDto.TransactionType,
      };

      _categoryService.AddCategory(newCategory);

      return RedirectToPage("./Index");
    }
  }
}