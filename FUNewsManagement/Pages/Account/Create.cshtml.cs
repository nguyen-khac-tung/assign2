using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class CreateModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public CreateModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public AccountDetailVM Account { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var isExistEmail = _accountRepository.CheckEmailExist(Account);
                if (isExistEmail)
                {
                    ViewData["Message"] = "Email is already used for another user.";
                }
                else
                {
                    ViewData["Success"] = "You have successfully created a account!";
                    _accountRepository.AddAccount(Account);
                }
            }
            return Page();
        }
    }
}
