using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public class NewsRepository: INewsRepository
    {
        private readonly NewsDBContext _context;

        public NewsRepository(NewsDBContext dbContext)
        {
            _context = dbContext;
        }

        public List<NewsPreviewVM> GetAllListNews()
        {
            var newsList = (from n in _context.NewsArticles
                            join c in _context.Categories on n.CategoryId equals c.CategoryId
                            where n.NewsStatus == true && n.IsDelete == false
                            select new NewsPreviewVM()
                            {
                                NewsArticleId = n.NewsArticleId,
                                NewsTitle = n.NewsTitle,
                                Headline = n.Headline,
                                CreatedDate = n.CreatedDate,
                                Category = c
                            }).OrderByDescending(n => n.CreatedDate).ToList();
            return newsList;
        }

        public List<NewsPreviewVM> GetAllListNewsForStaff()
        {
            var newsList = (from n in _context.NewsArticles
                            join c in _context.Categories on n.CategoryId equals c.CategoryId
                            where n.IsDelete == false
                            select new NewsPreviewVM()
                            {
                                NewsArticleId = n.NewsArticleId,
                                NewsTitle = n.NewsTitle,
                                Headline = n.Headline,
                                CreatedDate = n.CreatedDate,
                                Category = c,
                                CreatedById = n.CreatedById,
                                Status = (n.NewsStatus == true ? "Active" : "InActive")
                            }).OrderByDescending(n => n.CreatedDate).ToList();
            return newsList;
        }

        public List<NewsPreviewVM> GetNewsUserCreated(int accountId)
        {
            var newsList = GetAllListNewsForStaff().Where(n => n.CreatedById == accountId).OrderByDescending(n => n.CreatedDate).ToList();
            return newsList;
        }

        public NewsVM GetNewsById(string id)
        {
            var news = (from n in _context.NewsArticles
                        where n.NewsArticleId == id
                        select new NewsVM()
                        {
                            NewsArticleId = n.NewsArticleId,
                            NewsTitle = n.NewsTitle,
                            Headline = n.Headline,
                            NewsContent = n.NewsContent,
                            NewsSource = n.NewsSource,
                            CategoryId = n.Category.CategoryId,
                            NewsStatus = n.NewsStatus
                        }).FirstOrDefault();
            news.Tags = _context.NewsTags.Where(nt => nt.NewsArticleId == news.NewsArticleId).Select(nt => nt.TagId).ToList();

            return news;
        }

        public NewsDetailVM? GetNewsDetail(string id)
        {
            var news = (from n in _context.NewsArticles
                        join c in _context.Categories on n.CategoryId equals c.CategoryId
                        join a in _context.SystemAccounts on n.CreatedById equals a.AccountId
                        where n.NewsArticleId == id
                        select new NewsDetailVM()
                        {
                            NewsArticleId = n.NewsArticleId,
                            NewsTitle = n.NewsTitle,
                            Headline = n.Headline,
                            CreatedDate = n.CreatedDate,
                            ModifiedDate = n.ModifiedDate,
                            NewsContent = n.NewsContent,
                            NewsSource = n.NewsSource,
                            CreatedBy = a,
                            Category = c,
                        }).FirstOrDefault();
            news.Tags = _context.NewsTags.Where(nt => news.NewsArticleId == nt.NewsArticleId).Select(n => _context.Tags.Where(t => t.TagId == n.TagId).FirstOrDefault()).ToList();
            return news;
        }

        public void AddNewsArticle(NewsVM newsVM)
        {
            NewsArticle news = new NewsArticle();
            news.NewsArticleId = newsVM.NewsArticleId;
            news.NewsTitle = newsVM.NewsTitle;
            news.Headline = newsVM.Headline;
            news.NewsContent = newsVM.NewsContent;
            news.NewsSource = newsVM.NewsSource ?? "N/A";
            news.CreatedDate = DateTime.Now;
            news.NewsStatus = true;
            news.CategoryId = newsVM.CategoryId;
            news.CreatedById = newsVM.CreateByAccountId;
            news.IsDelete = false;
            _context.NewsArticles.Add(news);
            _context.SaveChanges();

            if (!newsVM.Tags.IsNullOrEmpty())
            {
                var newsTags = newsVM.Tags.Select(tagId => new NewsTag
                {
                    TagId = tagId,
                    NewsArticleId = news.NewsArticleId
                }).ToList();

                _context.NewsTags.AddRange(newsTags);
                _context.SaveChanges();
            }
        }

        public void EditNewsArticle(NewsVM newsVM)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var oldNews = _context.NewsArticles.First(n => n.NewsArticleId == newsVM.NewsArticleId);
                    oldNews.NewsTitle = newsVM.NewsTitle;
                    oldNews.Headline = newsVM.Headline;
                    oldNews.NewsContent = newsVM.NewsContent;
                    oldNews.NewsSource = newsVM.NewsSource ?? "N/A";
                    oldNews.ModifiedDate = DateTime.Now;
                    oldNews.NewsStatus = newsVM.NewsStatus;
                    oldNews.CategoryId = newsVM.CategoryId;
                    oldNews.UpdatedById = newsVM.UpdateByAccountId;
                    _context.SaveChanges();

                    var oldNewsTags = _context.NewsTags.Where(nt => nt.NewsArticleId == newsVM.NewsArticleId).ToList();
                    _context.NewsTags.RemoveRange(oldNewsTags);
                    _context.SaveChanges();

                    if (!newsVM.Tags.IsNullOrEmpty())
                    {
                        var newsTags = newsVM.Tags.Select(tagId => new NewsTag
                        {
                            TagId = tagId,
                            NewsArticleId = newsVM.NewsArticleId
                        }).ToList();

                        _context.NewsTags.AddRange(newsTags);
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public void DeleteNewsArticle(string id)
        {
            var news = _context.NewsArticles.First(n => n.NewsArticleId == id);
            news.IsDelete = true;
            _context.SaveChanges();
        }

        public List<NewsPreviewVM> SearchNewsArticle(string keyword)
        {
            var newsList = (from n in _context.NewsArticles
                            join c in _context.Categories on n.CategoryId equals c.CategoryId
                            where n.NewsStatus == true &&
                            (n.NewsTitle.ToLower().Contains(keyword) || n.Headline.ToLower().Contains(keyword))
                            select new NewsPreviewVM()
                            {
                                NewsArticleId = n.NewsArticleId,
                                NewsTitle = n.NewsTitle,
                                Headline = n.Headline,
                                CreatedDate = n.CreatedDate,
                                Category = c
                            }).ToList();
            return newsList;
        }

        public List<NewsStatistic> GetStatistic(string from, string to)
        {
            DateTime fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            var listAccount = _context.SystemAccounts.Where(a => a.AccountRole == 1).Select(a => new AccountDetailVM()
            {
                AccountId = a.AccountId,
                AccountName = a.AccountName,
                AccountEmail = a.AccountEmail,
                IsActive = a.IsActive,
                Status = (a.IsActive == true ? "Active" : "InActive")
            }).ToList();

            var listNews = _context.NewsArticles.Where(n => n.CreatedDate >= fromDate
            && n.CreatedDate <= toDate).ToList();

            var list = new List<NewsStatistic>();

            foreach (var item in listAccount)
            {
                NewsStatistic newsStatistic = new NewsStatistic();
                newsStatistic.account = item;
                newsStatistic.NewsAmount = listNews.Where(n => n.CreatedById == item.AccountId).ToList().Count;
                list.Add(newsStatistic);
            }
            return list;
        }
    }
}
