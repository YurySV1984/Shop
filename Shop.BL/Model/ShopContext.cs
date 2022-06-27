using System.Data.Entity;

namespace Shop.BL.Model
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("name=ShopConnectionString") { }

        public DbSet<Check>? Checks { get; set; }
        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Sell>? Sells { get; set; }
        public DbSet<Seller>? Sellers { get; set; }
    }
}
