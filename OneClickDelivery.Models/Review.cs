using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [StringLength(80)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        
        public int Rate { get; set; }

        public Resturant Resturant { get; set; }
        public int ResturantId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
    
}
