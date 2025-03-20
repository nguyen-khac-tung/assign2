using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public DetailsModel(ICategoryRepository categoryRepository)
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
    }
}
