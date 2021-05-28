using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class OrderDetail
    { 
    
        [Key]
        public int SubOrderId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }
        public virtual Order Order { get; set; }
    }
}