using BlazeCart.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace BlazeCart.Services
{
    public class CartService
    {
        static SQLiteAsyncConnection db;
        private ObservableCollection<Cart> _cartList = new();
        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Cart>();

        }

        public async Task AddCartToDb(string cartName, ObservableCollection<Item> cartItems, int cartItemsCount, double cartTotalPrice)
        {
            await Init();
            if (string.IsNullOrEmpty(cartName))
                cartName = "untitled";

            var cart = new Cart()
            {
                Name = cartName,
                CartItems = cartItems,
                Image = "cart_option_logo.png",
                ItemsCount = cartItemsCount,
                TotalPrice = cartTotalPrice

            };
            await db.InsertAsync(cart);
            await Shell.Current.DisplayAlert("Išsaugota!", "Krepšelis sėkmingai išsaugotas!", "OK");
        }

        public async Task RemoveCartFromDb(int  id)
        {
            await db.DeleteAsync<Cart>(id);
            await Init();
        }

        public async Task<IEnumerable<Cart>> GetCartsFromDb()
        {
            await Init();
           var cart = await db.Table<Cart>().ToListAsync();
            return cart;
        }
      
    
        public ObservableCollection<Cart> GetCarts()
        {
            return _cartList;
        }
    }
}
