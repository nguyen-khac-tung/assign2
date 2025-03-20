using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels;

namespace FUNewsManagement.Pages.Account
{
    public class ListModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public ListModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public List<AccountDetailVM> Accounts { get; set; }

        public IActionResult OnGet()
        {
            Accounts = _accountRepository.GetAllAccount();
            return Page();
        }
    }
}
