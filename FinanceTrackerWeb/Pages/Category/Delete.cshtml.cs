﻿using FinanceTracker.Domain.Abstractions;
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

    public IActionResult OnGetAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = _service.GetCategoryById(id.Value);

      if (category == null)
      {
        return NotFound();
      }

      Category = category;

      return Page();
    }

    public IActionResult OnPostAsync(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var category = _service.GetCategoryById(id.Value);
      if (category == null) return RedirectToPage("./Index");

      Category = category;
      _service.DeleteCategoryById(Category.Id);

      return RedirectToPage("./Index");
    }
  }
}