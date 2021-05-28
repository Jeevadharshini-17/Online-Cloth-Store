using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }

        public string CustomerAddress { get; set; }

        public string CustomerPhone { get; set; }

        public float CustomerWalletBalance { get; set; }
    }
}