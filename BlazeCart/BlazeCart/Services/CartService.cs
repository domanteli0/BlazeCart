using BlazeCart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazeCart.Services
{
    public class CartService
    {
        //This method should be used to get carts from file for particular user. For example, we can use it
        //to display user's saved carts history
        public async Task<ObservableCollection<Cart>> GetCarts(string filename)
        {
            //TO DO: How to find carts for particular user?
            using var stream = await FileSystem.OpenAppPackageFileAsync(filename);
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<ObservableCollection<Cart>>(contents);
        }

        //This method saves temporary cart to file, each user can have only ONE temporary cart
        public async Task ItemsToCart( string cartName)
        {
            Cart cart = new Cart();
            string _fileName = "cart.json";
            //TO DO: Check if the user already has a cart
            //if not create new cart
            //using FileStream createStream = File.Create(_fileName);
            //await JsonSerializer.SerializeAsync(createStream, cart);
            //await createStream.DisposeAsync();
        }

        //This method should be called when the user wants to save the cart
        public async Task SaveCart(Cart cart)
        {
            string _fileName = "savedCarts.json";
            using FileStream createStream = File.OpenWrite(_fileName);
            await JsonSerializer.SerializeAsync(createStream, cart);
            await createStream.DisposeAsync();
        }
    }
}
