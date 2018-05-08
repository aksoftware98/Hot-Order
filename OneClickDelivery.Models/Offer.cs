using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    [Table("Offers")]
    public class Offer : Item
    {
        public DateTime StartDate{ get; set; }
        public DateTime EndDate { get; set; }

        // Relationship with restuan
        public Resturant Resturant { get; set; }
        public int ResturantId { get; set;  }
    }
    
}
