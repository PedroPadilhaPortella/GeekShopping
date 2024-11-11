namespace GeekShopping.Web.Models
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long OrderHeaderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
