using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class City
    {
        public int CityId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [StringLength(50)]
        [Required]
        public string NameAr { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        public List<Resturant> Resturants { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
    }
    
}
