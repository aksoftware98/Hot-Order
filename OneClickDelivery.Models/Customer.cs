using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(25)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(25)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required]
        public string Email { get; set; }
        public bool IsBlocked { get; set; }

        [StringLength(50)]
        [Required]
        public string UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public DateTime RegistrationDate { get; set; }

        [StringLength(160)]
        [Required]
        public string ProfileImage { get; set; }

        [StringLength(160)]
        [Required]
        public string ProfileImageThump { get; set; }
        
        public City City { get; set; }
        public int? CityId { get; set; }

        public List<ShoppingCart> ShoppingCarts { get; set; }

        public List<Review> Reviews { get; set; }
    }
    
}
