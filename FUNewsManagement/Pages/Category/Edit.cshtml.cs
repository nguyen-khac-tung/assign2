using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ViewModels;

namespace FUNewsManagement.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public EditModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public CategoryDetailVM CategoryDetail { get; set; }

        public IActionResult OnGet(short id)
        {
            CategoryDetail = _categoryRepository.GetCategoryById(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ViewData["Success"] = "You have successfully edited a category!";
                _categoryRepository.EditCategory(CategoryDetail);
            }
            return Page();
        }
    }
}
