using System.Data.Entity;

namespace MorningCoffee.Models
{
    public class MorningCoffeeDbContext : DbContext
    {
        public MorningCoffeeDbContext() : base("MorningCoffeeDbContext") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.Subtotal).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.Tax).HasPrecision(18, 2);
            modelBuilder.Entity<Order>().Property(x => x.Total).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(x => x.UnitPrice).HasPrecision(18, 2);
            modelBuilder.Entity<OrderDetail>().Property(x => x.LineTotal).HasPrecision(18, 2);
            base.OnModelCreating(modelBuilder);
        }
    }
}
