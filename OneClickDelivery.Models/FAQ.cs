using System;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Faq
    {
        public int FaqId { get; set; }

        [StringLength(500)]
        [Required]
        public string Question { get; set; }

        [StringLength(500)]
        [Required]
        public string Answer { get; set; }

        public DateTime AddedDate { get; set; }
    }
    
}
