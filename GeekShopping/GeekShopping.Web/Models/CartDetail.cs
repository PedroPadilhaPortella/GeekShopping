using GeekShopping.Web.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Web.Models
{
    [Table("cart_detail")]
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("count")]
        public int Count { get; set; }
    }
}
