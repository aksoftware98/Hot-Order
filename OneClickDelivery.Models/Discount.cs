using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double Value { get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(40)]
        public string UserId { get; set; }

        public virtual List<Item> Items { get; set; }
    }
    
}
