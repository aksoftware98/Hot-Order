using System;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        [StringLength(80)]
        [Required]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public DateTime NotifyDate { get; set; }

        [StringLength(50)]
        public string SendUser { get; set; }

        [StringLength(50)]
        public string ReceiveUser { get; set; }

        [StringLength(160)]
        public string RedirectUrl { get; set; }
    }
    
}
