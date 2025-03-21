using FUNewsManagement.Hubs;
using FUNewsManagement.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace FUNewsManagement.Pages.Comment
{
    [IgnoreAntiforgeryToken]
    public class DeleteModel : PageModel
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHubContext<CommentHub> _hubContext;

        public DeleteModel(ICommentRepository commentRepository, IHubContext<CommentHub> hubContext)
        {
            _commentRepository = commentRepository;
            _hubContext = hubContext;
        }
        public IActionResult OnPost(string CommentId)
        {
            _commentRepository.DeleteComment(CommentId);
            _hubContext.Clients.All.SendAsync("CommentDeleted", CommentId);

            return StatusCode(200, new { });
        }
    }
}
