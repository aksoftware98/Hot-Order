using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    public class Resturant
    {
        public int ResturantId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        [StringLength(50)]
        [Required]
        public string OpenTime { get; set; }

        [StringLength(50)]
        [Required]
        public string CloseTime { get; set; }

        [StringLength(50)]
        [Required]
        public string OpenDays { get; set; }

        [StringLength(160)]
        [Required]
        public string Logo { get; set; }

        [StringLength(160)]
        [Required]
        public string LogoThump { get; set; }

        [StringLength(160)]
        [Required]
        public string CoverImage { get; set; }

        [StringLength(500)]
        [Required]
        public string AboutUs { get; set; }

        [StringLength(500)]
        public string UserId { get; set; }

        public DateTime RegistrationDate { get; set; }
        public bool IsBlocked { get; set; } = false; 

        public virtual Menu Menu { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; }

        public List<Contact> Contacts { get; set; }

        public List<Review> Reivews { get; set; }

        public List<Offer> Offers { get; set; }


    }

    
}
