using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [StringLength(160)]
        [Required]
        public string Value { get; set; }
        
        public Resturant Resturant { get; set; }
        public int ResturantId { get; set; }

        public ContactType ContactType { get; set; }
        public int ContactTypeId { get; set; }
    }
    
}
