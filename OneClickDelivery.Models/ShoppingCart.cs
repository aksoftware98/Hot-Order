using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneClickDelivery.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public DateTime CartDate { get; set; }
        public double TotalPrice { get; set; }
        public float Discount { get; set; }

        public Resturant Resturant { get; set; }
        public int ResturantId { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public virtual Order Order { get; set; }
    }
    
}
