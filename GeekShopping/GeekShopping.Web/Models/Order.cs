namespace GeekShopping.Web.Models
{
    public class Order
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public decimal PurchaseAmount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int CartTotalItems { get; set; }
        public bool PaymentStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
