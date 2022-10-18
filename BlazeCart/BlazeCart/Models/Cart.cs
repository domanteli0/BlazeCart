using System.Collections.ObjectModel;

namespace BlazeCart.Models
{
    [Serializable]
    public class Cart
    {
        public int CartId { get; set; }
        public string  Name { get; set; }
        public ObservableCollection<Item>  CartItems { get; set; }
        public string Image { get; set; }

        public Cart(int cartId, ObservableCollection<Item> cartItems, string name = "untitled")
        {
            CartId = cartId;
            CartItems = cartItems;
            Name = name;
            Image = "cart_option_logo.png";
        }
    }
}
