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
            string cartName = await Shell.Current.DisplayPromptAsync("Išsaugoti krepšelį", "Įveskite krepšelio pavadinimą: ", "OK",
                "Cancel", "Įveskite pavadinimą...");
            Cart cart = new(1,  CartItems, cartName);
            await _cartService.SaveCart(cart);

        }

        [RelayCommand]
        async Task LoadCommand()
        {
            
        }

        [RelayCommand]
        async void CheapestStore(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CheapestStorePage));
        }
    }
}
