using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineClothStore.Models
{
    public class OnlineClothStoreDBContext:DbContext
    {
        public OnlineClothStoreDBContext():base("DefaultConnection")
        {

        }
       
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }


    }
}