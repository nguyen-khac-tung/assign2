using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class CategoryDetailVM
    {
        public short? CategoryId { get; set; }

        [Required(ErrorMessage = "Category name can not be empty.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Account name has from 5 to 30 characters.")]
        public string? CategoryName { get; set; }

        [Required(ErrorMessage = "Category desciption can not be empty.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Category desciption has from 5 to 200 characters.")]
        public string? CategoryDesciption { get; set; }

        public short? ParentCategoryId { get; set; }

        public bool? IsActive { get; set; }

        public string? Status { get; set; }
    }
}
