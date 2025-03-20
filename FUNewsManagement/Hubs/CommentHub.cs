using Microsoft.AspNetCore.SignalR;
using ViewModels;

namespace FUNewsManagement.Hubs
{
    public class CommentHub: Hub
    {
        public async Task SendComment(CommentVM comment)
        {
            await Clients.All.SendAsync("ReceiveComment", comment);
        }

        public async Task EditComment(string commentId, string content)
        {
            await Clients.All.SendAsync("CommentEdited", commentId, content);
        }

        public async Task DeleteComment(string commentId)
        {
            await Clients.All.SendAsync("CommentDeleted", commentId);
        }
    }
}
