using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class MenuSection
    {
        public int MenuSectionId { get; set; }

        public virtual MenuType MenuType { get; set; }
        public int? MenuTypeId { get; set; }

        public Menu Menu { get; set; }
        public int? MenuId { get; set; }

        [Required]
        [StringLength(40)]
        public string UserId { get; set; }

        public List<Food> Food { get; set; }
    }
    
}
