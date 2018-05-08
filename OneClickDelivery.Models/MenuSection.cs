using System.Collections.Generic;

namespace OneClickDelivery.Models
{
    public class MenuSection
    {
        public int MenuSectionId { get; set; }

        public MenuType MenuType { get; set; }
        public int? MenuTypeId { get; set; }

        public Menu Menu { get; set; }
        public int? MenuId { get; set; }

        public List<Food> Food { get; set; }
    }
    
}
