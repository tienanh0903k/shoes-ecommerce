using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.DAL.Data
{
    public class ShoesStoreAppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Blog> Blog { get; set; }

        public DbSet<Size> Size { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ImageSystem> Image { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }



        public virtual DbSet<RefreshToken> RefreshToken { get; set; }



        public ShoesStoreAppDbContext(DbContextOptions<ShoesStoreAppDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;Database=ShoesStoreApp_DB1;Trusted_Connection=True;TrustServerCertificate=True")
        //             .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Remove/ Ignore UserRole (Because it create relationship N-N between User and Role. Here we want there to be a relationship 1-N between them)
            modelBuilder.Ignore<IdentityUserRole<Guid>>();


            modelBuilder.Entity<User>()
                        .HasOne(u => u.Role)
                        .WithMany(r => r.Users)
                        .HasForeignKey(u => u.RoleId);

            // Config when delete brand, don't delete product
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.Brand)
                        .WithMany(b=>b.Products)
                        .HasForeignKey(p => p.BrandId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Config 1 non-cluster index for discount 
            modelBuilder.Entity<Discount>()
                        .HasIndex(d => new { d.ProductId, d.StartDate, d.EndDate })
                        .IsUnique();


            // Config relationship 1-1 Cart and User
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .IsRequired();

            // Config Size
            modelBuilder.Entity<Size>().HasKey(s => new { s.SizeId, s.ProductId });

            // Config Review
            modelBuilder.Entity<Review>().HasKey(r => new { r.ProductId, r.UserId });

            modelBuilder.Entity<Review>()
                        .HasOne<Product>(r => r.Product)
                        .WithMany(p => p.Reviews)
                        .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<Review>()
                        .HasOne<User>(r => r.User)
                        .WithMany(u => u.Reviews)
                        .HasForeignKey(r => r.UserId);

            // Config OrderItem
            modelBuilder.Entity<OrderItem>().HasKey(oi => new { oi.OrderId, oi.ProductId, oi.Size });

            modelBuilder.Entity<OrderItem>()
                        .HasOne<Order>(oi => oi.Order)
                        .WithMany(o => o.Items)
                        .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                        .HasOne<Product>(oi => oi.Product)
                        .WithMany(p => p.Items)
                        .HasForeignKey(oi => oi.ProductId);

            // Config CartItem
            modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.CartId, ci.ProductId, ci.Size });

            modelBuilder.Entity<CartItem>()
                        .HasOne<Cart>(ci => ci.Cart)
                        .WithMany(c => c.Items)
                        .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItem>()
                        .HasOne<Product>(ci => ci.Product)
                        .WithMany(p => p.CartItems)
                        .HasForeignKey(ci => ci.ProductId);

            // modelBuilder.Seed();
        }
    }
}
