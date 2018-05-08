using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class BlockedCountry
    {
        public int BlockedCountryId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(25)]
        [Required]
        public string IP { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
    
}
