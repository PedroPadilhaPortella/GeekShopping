using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Models.Context
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}
