using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class MenuType
    {
        public int MenuTypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(160)]
        [Required]
        public string PicturePath { get; set; }

        public List<MenuSection> MenuSections { get; set; }
    }
    
}
