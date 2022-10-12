using System.Collections.ObjectModel;
using System.Windows.Input;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        private CartService _cartService = new CartService();

        public Cart cart { get; set; }

        public ObservableCollection<Item> CartItems { get; set; }

        [RelayCommand]
        async void Remove(object obj)
        {
            //remove an item from current cart

            //and then refresh page
        }

        [RelayCommand]
        async void Save(object obj)
        {
            //sample cart to save
            Cart _cart = new Cart();
           // string result = await  Application.Current.MainPage.DisplayPromptAsync("Save cart", "Enter cart name: ", "OK", "Cancel");
            await _cartService.SaveCart(_cart);

        }

        [RelayCommand]
        async Task LoadCommand()
        {
            var carts = await _cartService.GetCarts("cart.json");
            //TO DO: implement matching by user ID
            foreach (var cart in carts)
            {
                //TO DO: match user ID
                cart.CartItems = CartItems;
            }
        }

        [RelayCommand]
        async void CheapestStore(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CheapestStorePage));
        }
    }
}
