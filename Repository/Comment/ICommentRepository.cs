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
    }
}
