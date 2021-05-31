using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
        public string CategoryName { get; set; }

    } 
}