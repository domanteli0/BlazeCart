using BlazeCart.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace BlazeCart.Services
{
    public class CartService
    {
        private ObservableCollection<Cart> cartList = new();
        //This method should be used to get carts from file for particular user. For example, we can use it
        //to display user's saved carts history
        public async Task<ObservableCollection<Cart>> GetCarts(string filename)
        {
            //TO DO: How to find carts for particular user?
            using var stream = await FileSystem.OpenAppPackageFileAsync(filename + ".json");
            using (StreamReader r = new StreamReader(stream))
            {
                string json = r.ReadToEnd();
                var jobj = JObject.Parse(json);
                cartList = JsonConvert.DeserializeObject<ObservableCollection<Cart>>(jobj[filename].ToString());
                return cartList;
            }
        }

        //This method saves temporary cart to file, each user can have only ONE temporary cart
        public async Task ItemsToCart( string cartName)
        {

        }

        //This method should be called when the user wants to save the cart
        public async Task SaveCart(Cart cart)
        {

        }
    }
}
