using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Account
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public IActionResult OnPost(int id)
        {
            _accountRepository.DeleteAccount(id);
            return RedirectToPage("/Account/List");
        }
    }
}
