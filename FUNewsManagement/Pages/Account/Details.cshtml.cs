using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public DetailsModel(IAccountRepository accountRepository)
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
    }
}
