using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneClickDelivery.Website.ViewModels
{

    public enum MessageType
    {
        Success, Info, Warning, Error
    }

    public class MessageViewModel
    {
        public MessageType MessageType = MessageType.Success;

        public string Message;

    }
}