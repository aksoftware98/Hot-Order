using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class AppType
    {
        public int AppTypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public List<AppVersion> AppVersions { get; set; }
    }
    
}
