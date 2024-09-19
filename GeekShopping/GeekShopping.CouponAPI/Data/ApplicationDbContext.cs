using GeekShopping.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                Id = 1,
                Code = "ERUDIO_2022_10",
                DiscountAmount = 10
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon {
                Id = 2,
                Code = "ERUDIO_2022_15",
                DiscountAmount = 15
            });
        }
    }
}
