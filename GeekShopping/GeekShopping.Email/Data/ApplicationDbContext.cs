using GeekShopping.Email.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<EmailLog> Emails { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
