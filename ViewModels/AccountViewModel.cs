using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AccountDetailVM
    {
        public int? AccountId { get; set; }

        [Required(ErrorMessage = "Account name can not be empty.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Account name has from 5 to 50 characters.")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Email can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string AccountEmail { get; set; }

        [Required(ErrorMessage = "Account password can not be empty.")]
        public string? AccountPassword { get; set; }

        public int? AccountRole { get; set; }

        public string? RoleName { get; set; }

        public bool? IsActive { get; set; }

        public string? Status { get; set; }
    }

    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Email can not be empty.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string? AccountEmail { get; set; }

        [Required(ErrorMessage = "Password can not be empty.")]
        public string? AccountPassword { get; set; }
    }
}
