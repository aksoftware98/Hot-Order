using System.ComponentModel.DataAnnotations;

namespace OneClickDelivery.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public Item Item { get; set; }
        public int? ItemId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartId { get; set; }
    }
    
}
