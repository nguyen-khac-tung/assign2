using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.News
{
    public class SearchModel : PageModel
    {
        public IActionResult OnPost(string keyword)
        {
            return RedirectToPage("/Home/Index", new { keyword = keyword });
        }
    }
}
