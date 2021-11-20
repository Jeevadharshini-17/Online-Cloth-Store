using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string CustomerEmail { get; set; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string CustomerPassword { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string CustomerAddress { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number")]
        public string CustomerPhone { get; set; }

        public float CustomerWalletBalance { get; set; }
    }
}