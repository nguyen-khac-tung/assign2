using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class ListModel : PageModel
    {
        private readonly INewsRepository _newsRepository;

        public ListModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [BindProperty]
        public List<NewsPreviewVM> NewsList { get; set; }
        
        public IActionResult OnGet()
        {
            NewsList = _newsRepository.GetAllListNewsForStaff();
            return Page();
        }
    }
}
