using FUNewsManagement.Hubs;
using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ViewModels;

namespace FUNewsManagement.Pages.Comment
{
    [IgnoreAntiforgeryToken]
    public class CreateModel : PageModel
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHubContext<CommentHub> _hubContext;

        public CreateModel(ICommentRepository commentRepository, IAccountRepository accountRepository, IHubContext<CommentHub> hubContext)
        {
            _commentRepository = commentRepository;
            _accountRepository = accountRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnPost(CommentVM commentVM)
        {
            commentVM.CommentID = Guid.NewGuid().ToString();
            _commentRepository.CreateComment(commentVM);
            _hubContext.Clients.All.SendAsync("ReceiveComment", commentVM);
            return StatusCode(200, new { success = true });
        }
    }
}
