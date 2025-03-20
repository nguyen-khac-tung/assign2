using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Category
{
    public class ListModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [BindProperty]
        public List<CategoryDetailVM> CategoryDetails { get; set; }

        public IActionResult OnGet()
        {
            CategoryDetails = _categoryRepository.GetAllCategory();
            return Page();
        }
    }
}
