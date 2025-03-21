using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly NewsDBContext _context;

        public CommentRepository(NewsDBContext dbContext)
        {
            _context = dbContext;
        }

        public List<CommentVM> GetCommentOfNews(string newsId)
        {
            var listComment = (from c in _context.Comments
                               join a in _context.SystemAccounts on c.AccountId equals a.AccountId
                               where c.NewsArticleId == newsId && c.IsDelete == false
                               select new CommentVM()
                               {
                                   CommentID = c.CommentId,
                                   Content = c.Content,
                                   CreatedDate = c.CreatedDate,
                                   NewsArticleID = c.NewsArticleId,
                                   AccountID = a.AccountId,
                                   AccountName = a.AccountName
                               }).ToList();
            return listComment;
        }

        public void CreateComment(CommentVM comment)
        {
            Comment newComment = new Comment();
            newComment.CommentId = comment.CommentID;
            newComment.Content = comment.Content;
            newComment.CreatedDate = DateTime.Now;
            newComment.NewsArticleId = comment.NewsArticleID;
            newComment.AccountId = comment.AccountID;
            newComment.IsDelete = false;

            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public void EditComment(CommentVM comment)
        {
            var oldComment = _context.Comments.First(c => c.CommentId == comment.CommentID);
            oldComment.Content = comment.Content;

            _context.SaveChanges();
        }

        public void DeleteComment(string id)
        {
            var oldComment = _context.Comments.First(c => c.CommentId == id);
            oldComment.IsDelete = true;

            _context.SaveChanges();
        }
    }
}
