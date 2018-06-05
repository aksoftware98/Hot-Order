using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    [Table("Items")]
    public abstract class Item
    {
        public int ItemId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public double Cost { get; set; }
        public DateTime AddedDate { get; set; }

        [Required]
        [StringLength(40)]
        public string UserId { get; set; }

        [StringLength(160)]
        [Required]
        public string ItemImage { get; set; }

        [StringLength(160)]
        [Required]
        public string ItemImageThump { get; set; }

        public virtual List<Discount> Discounts { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
    
}
