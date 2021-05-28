using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Inventory
    {
        [ForeignKey("Product")]
        [Key]
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }

        public float ProductPrice { get; set; }
        public virtual Product  Product {get; set; }
    }
}