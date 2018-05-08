using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }
    }
    
}
