using CartAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace CartAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {            
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .Property(c => c.Id)
                .ValueGeneratedNever(); //nunca vai gerar o Id

            modelBuilder.Entity<Product>()
                .Property(c => c.Name)
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(c => c.Description)
                .HasMaxLength(250);

            modelBuilder.Entity<Product>()
                .Property(c => c.ImageURL)
                .HasMaxLength(250);

            modelBuilder.Entity<Product>()
                .Property(c => c.CategoryName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(c => c.Price)
                .HasPrecision(10, 2);

            //CartHeader
            modelBuilder.Entity<CartHeader>()
                .Property(c => c.UserId)
                .HasMaxLength(100);

            modelBuilder.Entity<CartHeader>()
                .Property(c => c.CouponCode)
                .HasMaxLength(100);


        }
    }
}
