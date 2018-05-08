using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    public class Menu
    {
        public int MenuId { get; set; }

        [StringLength(160)]
        [Required]
        public string MenuImage { get; set; }

        public virtual Resturant Resturant { get; set; }

        
   
        public List<MenuSection> MenuSections { get; set; }
    }
}
