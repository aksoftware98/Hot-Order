using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneClickDelivery.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double TotalPrice { get; set; }
        public float Discount { get; set; }
        public DateTime OrderDate { get; set; }
        [StringLength(250)]
        [Required]
        public string Street { get; set; }

        [StringLength(50)]
        [Required]
        public string Building { get; set; }

        [StringLength(50)]
        [Required]
        public string Home { get; set; }
        public double Longtiude { get; set; }
        public double Latitude { get; set; }

        public bool IsCouponUsed { get; set; } = false;

        [StringLength(50)]
        public string OrderTime { get; set; }

        [StringLength(50)]
        [Required]
        public string ApproximateTime { get; set; }

        [StringLength(50)]
        public string CouponCode { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public int OrderStatusId { get; set; }

        
    }
}
