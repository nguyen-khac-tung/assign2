using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public ProfileModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public AccountDetailVM? Account { get; set; }

        public IActionResult OnGet()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            Account = _accountRepository.GetAccountByEmail(email);
            return Page();
        }
    }
}
