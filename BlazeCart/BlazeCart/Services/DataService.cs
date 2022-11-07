using BlazeCart.Models;
using SQLite;
using System.Collections.ObjectModel;
using SQLiteNetExtensionsAsync.Extensions;

namespace BlazeCart.Services
{
    public class DataService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<Cart>();
            await db.CreateTableAsync<Item>();

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
            await db.InsertWithChildrenAsync(cart);
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
            var cart = await db.GetAllWithChildrenAsync<Cart>();
            return cart;
        }

        
        public async Task AddFavoriteItemToDb(Item favoriteItem)
        {
            await Init();

            //Check if favorite item already exist in db
            //TODO:Fix bug with adding to favorites (because of ItemId)
            var query = db.Table<Item>().Where(x => x.ItemId == favoriteItem.ItemId);
            var result = await query.ToListAsync();
            if (result.Count == 0)
            {
                await db.InsertAsync(favoriteItem);
            }
        }

        public async Task RemoveFavoriteItemFromDb(int itemId)
        {
            await Init();
            await db.DeleteAsync<Item>(itemId);
        }


        public async Task<IEnumerable<Item>> GetFavoriteItemsFromDb()
        {
            await Init();
            var favoriteItems = await db.Table<Item>().ToListAsync();
            return favoriteItems;
        }

    }
}
