using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class CreateModel : PageModel
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITagRepository _tagRepository;

        public CreateModel(INewsRepository newsRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IAccountRepository accountRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _tagRepository = tagRepository;
        }

        [BindProperty]
        public NewsVM NewsVM { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Categories"] = _categoryRepository.GetAllCategory();
            ViewData["Tags"] = _tagRepository.GetAllTag();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("UserEmail");
                var account = _accountRepository.GetAccountByEmail(email);
                NewsVM.NewsArticleId = Guid.NewGuid().ToString();
                NewsVM.CreateByAccountId = account?.AccountId;
                _newsRepository.AddNewsArticle(NewsVM);
                ViewData["Success"] = "You have successfully created a article!";
            }
            ViewData["Categories"] = _categoryRepository.GetAllCategory();
            ViewData["Tags"] = _tagRepository.GetAllTag();
            return Page();
        }
    }
}
