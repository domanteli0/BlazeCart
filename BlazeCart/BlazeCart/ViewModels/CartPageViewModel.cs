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
        private readonly CartService _cartService;

        public Cart Cart { get; set; }

        public ObservableCollection<Item> CartItems { get; set; } = new();

        public CartPageViewModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [RelayCommand]
        void Remove(Item item)
        {
            CartItems.Remove(item);
        }

        [RelayCommand]
        async void Save(object obj)
        {
            Cart cart;
            if(CartItems.Count > 0)
            {
                string cartName = await Shell.Current.DisplayPromptAsync("Išsaugoti krepšelį", "Įveskite krepšelio pavadinimą: ", "OK",
               "Cancel", "Įveskite pavadinimą...");
                if (string.IsNullOrEmpty(cartName))
                {
                    cart = new(cartId: 1, cartItems: CartItems);
                }
                else
                {
                    cart = new(cartId: 1, cartItems: CartItems, name: cartName);
                }
                await _cartService.SaveCart(cart);
            }
            else
            {
                await Shell.Current.DisplayAlert("Klaida!", "Krepšelis tuščias!", "OK");
            }
        }


        [RelayCommand]
        async void CheapestStore(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CheapestStorePage));
        }
    }
}
