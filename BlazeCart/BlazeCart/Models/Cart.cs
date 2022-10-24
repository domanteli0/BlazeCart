using System.Collections.ObjectModel;
using SQLite;

namespace BlazeCart.Models
{
    [Serializable, Table("Carts")]
    public class Cart
    {
        [AutoIncrement, PrimaryKey]
        public int CartId { get; set; }
        public string  Name { get; set; }
        public ObservableCollection<Item>  CartItems { get; set; }
        public string Image { get; set; }

        public int ItemCount { get; set; }

        public double TotalPrice { get; set; }

        public Cart(int cartId, ObservableCollection<Item> cartItems, string name = "untitled")
        {
            CartId = cartId;
            CartItems = cartItems;
            Name = name;
            Image = "cart_option_logo.png";
            ItemCount = cartItems.Count;
            TotalPrice = GetCartPrice(CartItems);
        }

        private Double GetCartPrice(ObservableCollection<Item> cartItems)
        {
            Double TotalPrice = 0;
            foreach (Item I in cartItems)
            {
                TotalPrice += I.Price;
            }
            return TotalPrice;
        }
    }
}
