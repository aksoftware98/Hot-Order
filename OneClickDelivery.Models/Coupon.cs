using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(15)]
        [Required]
        public string Code { get; set; }

        public float Discount { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfTimes { get; set; }

    }
    
}
