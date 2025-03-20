using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Category
{
    public class DeleteModel : PageModel
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteModel(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public IActionResult OnPost(short id)
        {
            var isExist = _categoryRepository.CheckExistNewsOfCategory(id);
            if (!isExist)
            {
                _categoryRepository.DeleteCategory(id);
            }
            else
            {
                TempData["Message"] = "Can not delete this category";
            }
            return RedirectToPage("/Category/List");
        }
    }
}
