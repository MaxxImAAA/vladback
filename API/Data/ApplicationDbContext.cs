using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserAuth> UserAuths { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAuth>()
                .HasOne(x => x.User).WithOne(x => x.UserAuth).HasForeignKey<User>(x => x.UserAuthId);

            builder.Entity<User>()
                .HasOne(x => x.Cart).WithOne(x => x.User).HasForeignKey<Cart>(x => x.UserId);

            builder.Entity<User>()
                .HasMany(x => x.Responses).WithOne(x => x.User).HasForeignKey(x => x.UserId);

            builder.Entity<Product>()
                .HasMany(x => x.CartProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            builder.Entity<Cart>()
            .HasMany(x => x.CartProducts).WithOne(x => x.Cart).HasForeignKey(x => x.CartId);
        }
    }
}
