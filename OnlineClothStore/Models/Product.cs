using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Product
    {

        
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int VendorId { get; set; }
        public int CategoryId { get; set; }
        public string AdminStatus { get; set; }

        public byte[] ProductImage { get; set; }
        public virtual Category Category { get; set; }

        public virtual Inventory Inventory { get; set; }
    } 
    }