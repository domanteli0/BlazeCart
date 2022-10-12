using System.Collections.ObjectModel;
using System.Windows.Input;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;

namespace BlazeCart.ViewModels
{
    internal class CartPageViewModel : BaseViewModel
    {
        public ICommand RemoveCommand { get; set; }


        private CartService _cartService = new CartService();

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        public ICommand CheapestStoreCommand { get; set; }

        public Cart cart { get; set; }

        public ObservableCollection<Item> CartItems { get; set; }

        public CartPageViewModel()
        {
            RemoveCommand = new Command(OnRemoveCommand);
            SaveCommand = new Command(OnSaveCommand);
            LoadCommand  = new Command(async()=> await OnLoadCommand());
            CheapestStoreCommand = new Command(OnCheapestStoreCommand);
        }

        async void OnRemoveCommand(object obj)
        {
            //remove an item from current cart

            //and then refresh page
        }

        async void OnSaveCommand(object obj)
        {
            //sample cart to save
            Cart _cart = new Cart();
           // string result = await  Application.Current.MainPage.DisplayPromptAsync("Save cart", "Enter cart name: ", "OK", "Cancel");
            await _cartService.SaveCart(_cart);

        }

        async Task OnLoadCommand()
        {
            var carts = await _cartService.GetCarts("cart.json");
            //TO DO: implement matching by user ID
            foreach (var cart in carts)
            {
                //TO DO: match user ID
                cart.CartItems = CartItems;
            }
        }

        async void OnCheapestStoreCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CheapestStorePage));
        }
    }
}
