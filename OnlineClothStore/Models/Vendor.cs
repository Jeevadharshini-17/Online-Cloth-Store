using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string VendorName { get; set; }
        [Required]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        [DataType(DataType.Password)]
        public string VendorPassword { get; set; }
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string VendorEmail { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number")]
        public string VendorPhone { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string VendorAddress { get; set; }
        public float VWalletBalance { get; set; }
    }
}