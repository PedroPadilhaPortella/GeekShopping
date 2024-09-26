namespace GeekShopping.CartAPI.DTO
{
    public class CouponDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
