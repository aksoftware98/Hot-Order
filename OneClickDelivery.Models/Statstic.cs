using System;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Statstic
    {
        public int StatsticId { get; set; }

        [StringLength(50)]
        [Required]
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    
}
