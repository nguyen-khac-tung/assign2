using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.News
{
    public class EditModel : PageModel
    {
        private readonly INewsRepository _newsRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITagRepository _tagRepository;

        public EditModel(INewsRepository newsRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IAccountRepository accountRepository)
        {
            _newsRepository = newsRepository;
            _categoryRepository = categoryRepository;
            _accountRepository = accountRepository;
            _tagRepository = tagRepository;
        }

        [BindProperty]
        public NewsVM NewsVM { get; set; }

        public IActionResult OnGet(string id)
        {
            NewsVM = _newsRepository.GetNewsById(id);
            ViewData["Categories"] = _categoryRepository.GetAllCategory();
            ViewData["Tags"] = _tagRepository.GetAllTag();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("UserEmail");
                NewsVM.UpdateByAccountId = _accountRepository.GetAccountByEmail(email)?.AccountId;
                _newsRepository.EditNewsArticle(NewsVM);
                ViewData["Success"] = "You have successfully edited a article!";
            }
            ViewData["Categories"] = _categoryRepository.GetAllCategory();
            ViewData["Tags"] = _tagRepository.GetAllTag();
            return Page();
        }
    }
}
