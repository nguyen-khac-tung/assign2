using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public interface INewsRepository
    {
        public List<NewsPreviewVM> GetAllListNews();
        public List<NewsPreviewVM> GetAllListNewsForStaff();
        public List<NewsPreviewVM> GetNewsUserCreated(int accountId);
        public NewsVM GetNewsById(string id);
        public NewsDetailVM? GetNewsDetail(string id);

        public void AddNewsArticle(NewsVM newsVM);

        public void EditNewsArticle(NewsVM newsVM);

        public void DeleteNewsArticle(string id);

        public List<NewsPreviewVM> SearchNewsArticle(string keyword);
        public List<NewsStatistic> GetStatistic(string from, string to);
    }
}
