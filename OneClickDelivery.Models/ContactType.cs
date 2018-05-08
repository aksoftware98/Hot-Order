using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public List<Contact> Contacts { get; set; }
    }
    
}
