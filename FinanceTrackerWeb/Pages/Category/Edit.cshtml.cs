using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Infrastructure.Configuration;
using FinanceTrackerWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb.Pages.Category
{
  public class EditModel : PageModel
  {
    private readonly ICategoryService _categoryService;

    public EditModel(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    [BindProperty] public CategoryDto Category { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = await _categoryService.GetCategoryById(id.Value);
      if (category == null)
      {
        return NotFound();
      }

      Category = new CategoryDto
      {
        CategoryName = category.CategoryName,
        TransactionType = category.TransactionType
      };

      return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
      if (!ModelState.IsValid || id == null)
      {
        return Page();
      }

      var updatedCategory = new FinanceTracker.Domain.Entities.Category
      {
        Id = id.Value,
        CategoryName = Category.CategoryName,
        TransactionType = Category.TransactionType
      };

      await _categoryService.UpdateCategory(updatedCategory);

      return RedirectToPage("./Index");
    }
  }
}