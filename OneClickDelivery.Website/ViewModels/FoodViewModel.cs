using OneClickDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneClickDelivery.Website.ViewModels
{

    public class FoodViewModel
    {
        public MessageViewModel Message { get; set; }

        public FoodViewModel()
        {
            Message = new MessageViewModel { Message = "", MessageType = MessageType.Success }; 
        }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
        [StringLength(500)]
        public string Ingredients { get; set; }

        [Required]
        [Range(0,5000)]
        public double Cost { get; set; }
        
        [Required]  
        [StringLength(50)]
        public string Size { get; set; }

        public MenuSection MenuSection { get; set; } 

        public DateTime AddedDate { get; set; }

        public string PhotoPath { get; set; }

        public List<Food> Food { get; set; }
    }
}
