using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using ViewModels;

namespace FUNewsManagement.Pages.Category
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public CategoryDetailVM CategoryDetail { get; set; }  

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ViewData["Success"] = "You have successfully created a category!";
                _categoryRepository.AddCategory(CategoryDetail);
            }
            return Page();
        }
    }
}
