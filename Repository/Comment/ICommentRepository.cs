using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public interface ICommentRepository
    {
        public List<CommentVM> GetCommentOfNews(string newsId);
        public void CreateComment(CommentVM comment);
        public void EditComment(CommentVM comment);
        public void DeleteComment(string id);
    }
}
