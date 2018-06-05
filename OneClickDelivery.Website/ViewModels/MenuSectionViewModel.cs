using OneClickDelivery.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneClickDelivery.Website.ViewModels
{

   

    public class MenuSectionViewModel
    {

        public MenuSectionViewModel()
        {
            Message = new MessageViewModel
            {
                Message = "",
                MessageType = MessageType.Success
            };
        }

        public MessageViewModel Message { get; set; }
        public List<MenuSection> MenuSections { get; set; }

        public MenuSection MenuSection { get; set; }

        public List<Food> Food { get; set; }
        
        [Required(ErrorMessage = "Menu type is required*")] 
        [Display(Name = "Menu Type")]
        public int MenuTypeId { get; set; }

        public SelectList MenuTypes { get; set; }

        public Resturant Resturant { get; set; }

    }
}