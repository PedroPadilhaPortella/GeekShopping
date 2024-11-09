﻿using GeekShopping.Web.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.Web.Models
{
    [Table("coupon")]
    public class Coupon : BaseEntity
    {
        [Column("code")]
        [Required]
        [StringLength(30)]
        public string Code { get; set; }

        [Column("discount_amount")]
        [Required]
        public decimal DiscountAmount { get; set; }
    }
}
