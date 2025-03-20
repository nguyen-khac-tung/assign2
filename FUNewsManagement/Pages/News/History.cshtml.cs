using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class HistoryModel : PageModel
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITagRepository _tagRepository;

        public HistoryModel(INewsRepository newsRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IAccountRepository accountRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _tagRepository = tagRepository;
        }

        [BindProperty]
        public List<NewsPreviewVM> News { get; set; }

        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            var account = _accountRepository.GetAccountByEmail(email);
            News = _newsRepository.GetNewsUserCreated(account.AccountId ?? 0);
            return Page();
        }
    }
}
