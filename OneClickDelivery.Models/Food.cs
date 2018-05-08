using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    [Table("Food")]
    public class Food : Item
    {
        [StringLength(500)]
        public string Ingredients { get; set; }

        [StringLength(50)]
        public string Size { get; set; }
        
        public MenuSection MenuSection { get; set; }
        public int MenuSectionId { get; set; }
    }
    
}
