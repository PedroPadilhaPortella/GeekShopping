

using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.DTO
{
    public class CartHeaderDTO
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Card Number is required.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVV Code is required.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be exactly 3 numeric digits.")]
        public string CVV { get; set; }

        [Required(ErrorMessage = "Card Expire Date is required.")]
        [RegularExpression(@"^(0[1-9]|1[0-2])/(\d{2})$",
        ErrorMessage = "Expiration date must be in the format MM/YY.")]
        public string ExpireDate { get; set; }
    }
}
