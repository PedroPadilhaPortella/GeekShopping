using GeekShopping.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<OrderHeader> Headers { get; set; }
        public DbSet<OrderDetail> Details { get; set; }
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
