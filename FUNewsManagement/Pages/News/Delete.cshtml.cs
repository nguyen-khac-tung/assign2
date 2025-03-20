using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.News
{
    public class DeleteModel : PageModel
    {
        private readonly INewsRepository _newsRepository;

        public DeleteModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IActionResult OnPost(string id)
        {
            _newsRepository.DeleteNewsArticle(id);
            return RedirectToPage("/News/List");
        }
    }
}
