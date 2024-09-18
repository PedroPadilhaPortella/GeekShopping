using GeekShopping.CartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    
        public DbSet<CartHeader> CartHeaders { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
        }
    }
}
