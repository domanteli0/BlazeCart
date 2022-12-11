using BlazeCart.Models;
using SQLite;
using System.Collections.ObjectModel;
using SQLiteNetExtensionsAsync.Extensions;

namespace BlazeCart.Services
{
    public class DataService
    {
        static SQLiteAsyncConnection _db;

        static async Task Init()
        {
            if (_db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            _db = new SQLiteAsyncConnection(databasePath);
            await _db.CreateTableAsync<Cart>();
            await _db.CreateTableAsync<Item>();

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
            await _db.InsertWithChildrenAsync(cart);
            await Shell.Current.DisplayAlert("Išsaugota!", "Krepšelis sėkmingai išsaugotas!", "OK");
        }

        public async Task RemoveCartFromDb(int  id)
        {
            await _db.DeleteAsync<Cart>(id);
            await Init();
        }

        public async Task<IEnumerable<Cart>> GetCartsFromDb()
        {
            await Init();
            var cart = await _db.GetAllWithChildrenAsync<Cart>();

            return cart;
        }

        
        public async Task AddFavoriteItemToDb(Item favoriteItem)
        {
            await Init();

            Item item = await _db.Table<Item>().Where(x => x.NameLT == favoriteItem.NameLT && x.Store == favoriteItem.Store).FirstOrDefaultAsync();
            if (item != null)
            {
                item.IsFavorite = true;
                await _db.UpdateAsync(item);
            }
            else{
                await _db.InsertAsync(favoriteItem);
            }

        }

        public async Task RemoveFavoriteItemFromDb(int itemId)
        {
            await Init();
            await _db.DeleteAsync<Item>(itemId);
        }


        public async Task<IEnumerable<Item>> GetFavoriteItemsFromDb()
        {
            await Init();
            var favIt = await _db.GetAllWithChildrenAsync<Item>();

            var favoriteItems =  favIt.Where(item => item.IsFavorite == true);
            
            return favoriteItems;
        }

    }
}
