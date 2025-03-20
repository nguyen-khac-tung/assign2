using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class StatisticModel : PageModel
    {

        private readonly INewsRepository _newsRepository;

        public StatisticModel(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [BindProperty]
        public List<NewsStatistic> Statistics { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(string from, string to)
        {
            Statistics = _newsRepository.GetStatistic(from, to);
            return Page();
        }
    }
}
