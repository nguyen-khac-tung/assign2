using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public LoginModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public AccountLoginVM AccountLoginVM { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var account = _accountRepository.GetAccount(AccountLoginVM);

            if (account == null) { ViewData["Message"] = "Incorrect login information."; return Page(); }
            if (account?.IsActive == false) { ViewData["Message"] = "This account has been disabled."; return Page(); }

            HttpContext.Session.SetString("UserEmail", account.AccountEmail);
            HttpContext.Session.SetString("UserRole", account.RoleName);

            if (account.RoleName == "Admin") { return RedirectToPage("/Account/List"); }
            if (account.RoleName == "Staff") { return RedirectToPage("/News/List"); }
            return RedirectToPage("/Home/Index");
        }
    }
}
