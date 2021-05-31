namespace OnlineClothStore.Migrations
{
    using OnlineClothStore.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineClothStore.Models.OnlineClothStoreDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(OnlineClothStore.Models.OnlineClothStoreDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
           
            var customers = new List<Customer>
            {
                new Customer { CustomerName="Jeevadharshini", CustomerEmail="jeeva@gmail.com",CustomerPassword="customer@123",CustomerAddress="Coimbatore",CustomerPhone="1234567890",CustomerWalletBalance=0}
            };
            var vendors = new List<Vendor>
            {
                new Vendor { VendorName="Harinee", VendorEmail="harinee@gmail.com",VendorPassword="vendor@123",VendorAddress="Coimbatore",VendorPhone="1234567891",VWalletBalance=0}
            };
            var categories = new List<Category>
            {
                new Category { CategoryName="Women"}
            };
            var products = new List<Product>
            {
                new Product { ProductName="Denim Jackets", ProductImage=null,VendorId=1,CategoryId=1}
            };
            var inventories = new List<Inventory>
            {
                new Inventory { ProductId=1, ProductQuantity=10,ProductPrice=1000}
            };
            products.ForEach(p => context.Product.Add(p));
            
            categories.ForEach(ct => context.Category.Add(ct));
            customers.ForEach(c => context.Customer.Add(c));
            vendors.ForEach(v => context.Vendor.Add(v));
            inventories.ForEach(i => context.Inventory.Add(i));
            context.SaveChanges();
        }
    }
}
