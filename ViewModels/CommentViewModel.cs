using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CommentVM
    {
        public string CommentID { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? NewsArticleID { get; set; }
        public SystemAccount? Account { get; set; }
    }
}
