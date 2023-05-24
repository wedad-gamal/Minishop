using Microsoft.EntityFrameworkCore;
using Minishop.Infrastructure.Entities;

namespace Minishop.Infrastructure
{
    public class MinishopDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<RateItem> RateItems { get; set; }
        public DbSet<SizeType> SizeTypes { get; set; }

        public MinishopDBContext() { }
        public MinishopDBContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\ItShareAsp.netAngular\\projects\\Minishop\\DB\\minishop.mdf;Integrated Security=True;Connect Timeout=30");
            }


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
           .HasOne(e => e.Image)
           .WithOne(e => e.Product)
           .HasForeignKey<Image>(e => e.ProductId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
