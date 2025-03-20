using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class NewsVM
    {
        public string? NewsArticleId { get; set; }

        [Required(ErrorMessage = "Please input news title.")]
        [StringLength(100, MinimumLength = 20, ErrorMessage = "News title has from 20 to 100 characters.")]
        public string NewsTitle { get; set; }

        [Required(ErrorMessage = "Please input headline of news.")]
        [StringLength(100, MinimumLength = 20, ErrorMessage = "Headline has from 20 to 100 characters.")]
        public string Headline { get; set; }

        [Required(ErrorMessage = "Content of news can not be empty.")]
        [StringLength(1000, MinimumLength = 100, ErrorMessage = "Content has from 100 to 1000 characters.")]
        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        [Required(ErrorMessage = "News must belong to at least one category.")]
        public short CategoryId { get; set; }
        public List<int>? Tags { get; set; }
        public bool? NewsStatus { get; set; }
        public int? CreateByAccountId { get; set; }
        public int? UpdateByAccountId { get; set; }
    }

    public class NewsPreviewVM
    {
        public string NewsArticleId { get; set; } = null!;
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public Category? Category { get; set; }
        public int? CreatedById { get; set; }
        public string? Status { get; set; }
    }

    public class NewsDetailVM
    {
        public string NewsArticleId { get; set; }
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public SystemAccount CreatedBy { get; set; }
        public Category? Category { get; set; }
        public List<Tag?> Tags { get; set; }
    }

    public class NewsStatistic
    {
        public AccountDetailVM account { get; set; }
        public int NewsAmount { get; set; }
    }
}
