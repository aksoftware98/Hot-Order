using OneClickDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneClickDelivery.Website.ViewModels
{

    public enum MessageType
    {
        Success, Info, Warning
    }

    public class MenuSectionViewModel
    {
        public MessageType MessageType = MessageType.Success; 

        public string message;

        public MenuSection MenuSection { get; set; }

        public List<MenuType> MenuTypes { get; set; }

        public List<Food> Food { get; set; }
    }
}