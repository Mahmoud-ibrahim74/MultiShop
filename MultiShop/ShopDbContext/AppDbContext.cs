using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShop.Models;
using System.Data;
namespace MultiShop.ShopDbContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        //public DbSet<Item> Items { get; set; }
        //public DbSet<productions> productions { get; set; }
        //public DbSet<Employees> Employees { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<ReviewsProducts> reviews { get; set; }
        public DbSet<Cart> cart { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "User",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
