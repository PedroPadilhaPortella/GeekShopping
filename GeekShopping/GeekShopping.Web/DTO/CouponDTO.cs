using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.DTO
{
    public class CouponDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Coupon Code is required.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Discount Amount is required.")]
        public decimal DiscountAmount { get; set; }
    }
}
