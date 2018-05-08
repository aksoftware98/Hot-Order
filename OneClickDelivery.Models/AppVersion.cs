using System;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class AppVersion
    {
        public int AppVersionId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [StringLength(160)]
        public string Url { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public AppType AppType { get; set; }
        public int AppTypeId { get; set; }
    }
    
}
