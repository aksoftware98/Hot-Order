using OneClickDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneClickDelivery.Website.ViewModels
{
    public class OfferViewModel
    {
        public MessageViewModel Message { get; set; }

        public OfferViewModel()
        {
            Message = new MessageViewModel { Message = "", MessageType = MessageType.Success }; 
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0, 5000)]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public string PhotoPath { get; set; }

        public Resturant Resturant { get; set; }

        public List<Offer> Offers { get; set; }
    }
}