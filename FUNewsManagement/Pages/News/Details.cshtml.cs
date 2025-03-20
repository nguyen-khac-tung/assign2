using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICommentRepository _commentRepository;

        public DetailsModel(INewsRepository newsRepository, ICommentRepository commentRepository)
        {
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
        }

        [BindProperty]
        public NewsDetailVM DetailVM { get; set; }

        public IActionResult OnGet(string id)
        {
            ViewData["Role"] = HttpContext.Session.GetString("UserRole");
            DetailVM = _newsRepository.GetNewsDetail(id);
            ViewData["ListComment"] = _commentRepository.GetCommentOfNews(id);
            return Page();
        }
    }
}
