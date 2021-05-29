using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Vendor
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorPassword { get; set; }
        public string VendorEmail { get; set; }
        public string VendorPhone { get; set; }
        public string VendorAddress { get; set; }
        public float VWalletBalance { get; set; }
    }
}