using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        public int ProductQuantity { get; set; }

        public float ProductPrice { get; set; }
    }
}