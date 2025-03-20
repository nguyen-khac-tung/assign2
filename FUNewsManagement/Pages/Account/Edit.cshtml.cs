using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class EditModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public EditModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public AccountDetailVM Account { get; set; }

        public IActionResult OnGet(int id)
        {
            Account = _accountRepository.GetAccountById(id);
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
                    ViewData["Success"] = "You have successfully edited a account!";
                    _accountRepository.EditAccount(Account);
                }
            }
            return Page();
        }
    }
}
