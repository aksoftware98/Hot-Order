using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneClickDelivery.Website.Assistant
{
    public static class Extensions
    {
        // Extension Method to check if the file extension is for image file or not 
        public static bool IsImageExtension(this string Extension)
        {
            if (!Extension.ToLower().Contains(".jpg") && !Extension.ToLower().Contains(".png") && !Extension.ToLower().Contains(".bmp"))
                return false;

            return true; 
        }
    }
}