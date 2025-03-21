using FUNewsManagement.Hubs;
using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using ViewModels;

namespace FUNewsManagement.Pages.Comment
{
    [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IHubContext<CommentHub> _hubContext;

        public EditModel(ICommentRepository commentRepository, IAccountRepository accountRepository, IHubContext<CommentHub> hubContext)
        {
            _commentRepository = commentRepository;
            _accountRepository = accountRepository;
            _hubContext = hubContext;
        }

        public IActionResult OnPost(string CommentId, string Content)
        {
            var commentVM = new CommentVM()
            {
                CommentID = CommentId,
                Content = Content
            };

            _commentRepository.EditComment(commentVM);
            _hubContext.Clients.All.SendAsync("CommentEdited", CommentId, Content);

            return StatusCode(200, new { });
        }
    }
}
