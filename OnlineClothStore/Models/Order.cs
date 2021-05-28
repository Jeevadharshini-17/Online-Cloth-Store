﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public float OrderTotal { get; set; }
        public virtual List<OrderDetail> OrderDetails{ get; set; }
    }
}