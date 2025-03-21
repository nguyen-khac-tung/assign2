using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using ViewModels;
using Microsoft.IdentityModel.Tokens;
using FUNewsManagement.Repository;

namespace FUNewsManagement.Pages.Home
{
    public class IndexModel : PageModel
    {
        private readonly INewsRepository _newsRepository;

        public IndexModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [BindProperty]
        public List<NewsPreviewVM> NewsList { get; set; }

        public IActionResult OnGet(string keyword)
        {
            
            if (keyword.IsNullOrEmpty())
            {
                NewsList = _newsRepository.GetAllListNews();
            }
            else
            {
                NewsList = _newsRepository.SearchNewsArticle(keyword);
            }
            return Page();
        }
    }
}
