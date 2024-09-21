namespace GeekShopping.Web.Models
{
    public class Coupon
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
