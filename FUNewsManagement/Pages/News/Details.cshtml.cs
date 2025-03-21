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
        private readonly IAccountRepository _accountRepository;

        public DetailsModel(INewsRepository newsRepository, ICommentRepository commentRepository, IAccountRepository accountRepository)
        {
            _newsRepository = newsRepository;
            _commentRepository = commentRepository;
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public NewsDetailVM DetailVM { get; set; }

        public IActionResult OnGet(string id)
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var account = _accountRepository.GetAccountByEmail(email);
            ViewData["AccountId"] = account.AccountId;
            ViewData["Role"] = HttpContext.Session.GetString("UserRole");
            ViewData["ListComment"] = _commentRepository.GetCommentOfNews(id);
            DetailVM = _newsRepository.GetNewsDetail(id);
            return Page();
        }
    }
}
