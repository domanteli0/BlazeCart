using BlazeCart.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BlazeCart.Services
{
    public class CartService
    {
        private ObservableCollection<Cart> _cartList = new();
    
        public ObservableCollection<Cart> GetCarts()
        {
            return _cartList;
        }


        public async Task SaveCart(Cart cart)
        {
            _cartList.Add(cart);
            await Shell.Current.DisplayAlert("Išsaugota!", "Krepšelis sėkmingai išsaugotas!", "OK");
        }
    }
}
