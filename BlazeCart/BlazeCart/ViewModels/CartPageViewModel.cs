using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private ItemService _itemService;

        public Cart Cart { get; set; }

        public int ItemCount { get; set; }

        public ObservableCollection<Item> CartItems { get; set; } = new();

        public CartPageViewModel(DataService dataService, ItemService itemService)
        {
            _itemService = itemService;
            _itemService.CartUsed += CartUsedEventHandler;
            _dataService = dataService;
        }

        private void CartUsedEventHandler(object sender, CartUsedEventArgs e)
        {
            CartItems.Clear();
            foreach (var item in e.Items)
            {
                CartItems.Add(item);
            }
        }

        [RelayCommand]
        void Remove(Item item)
        {
            CartItems.Remove(item);
            _itemService.RemoveFromCart(item);
        }

        [RelayCommand]
        async void Save(object obj)
        {
            if (CartItems.Count > 0)
            {
                string cartName = await Shell.Current.DisplayPromptAsync("Išsaugoti krepšelį", "Įveskite krepšelio pavadinimą: ", "OK",
               "Cancel", "Įveskite pavadinimą...");
                foreach (var item in CartItems)
                {
                    item.IsFavorite = false;
                }
                await _dataService.AddCartToDb(cartName, CartItems, GetCartItemsCount(CartItems), GetCartPrice(CartItems));
           
                _itemService.OnCartTbUpdated(EventArgs.Empty);
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

        private static double GetCartPrice(ObservableCollection<Item> cartItems)
        {
            double totalPrice = 0;
            foreach (Item I in cartItems)
            {
                totalPrice += I.Price * (double)I.Quantity;
            }

            return totalPrice;
        }

        private int GetCartItemsCount(ObservableCollection<Item> cartItems)
        {
            int quantity = 0;
            foreach (var item in cartItems)
            {
                quantity += item.Quantity;
            }

            return quantity;
        }

        [RelayCommand]
        private void AddQuantity(Item item)
        {
            item.Quantity++;
        }
        [RelayCommand]
        private void RemoveQuantity(Item item)
        {
            item.Quantity--;
        }

    }
}
