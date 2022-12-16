using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace BlazeCart.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        private readonly DataService _dataService;

        private ItemService _itemService;

        public ObservableCollection<Item> CartItems { get; set; } = new();

        private ILogger<CartPageViewModel> _logger;

        public CartPageViewModel(DataService dataService, ItemService itemService, ILogger<CartPageViewModel> logger)
        {
            _itemService = itemService;
            _logger = logger;
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
            try
            {
                CartItems.Remove(item);
                _itemService.RemoveFromCart(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to remove item from cart: {item.ItemId}, {item.NameLT}, {ex.Message}");
                throw;
            }
        }

        [RelayCommand]
        async void Save(object obj)
        {
            try
            {
                if (CartItems.Count > 0)
                {
                    string cartName = await Shell.Current.DisplayPromptAsync("Išsaugoti krepšelį",
                        "Įveskite krepšelio pavadinimą: ", "OK",
                        "Cancel", "Įveskite pavadinimą...");
                    foreach (var item in CartItems)
                    {
                        item.IsFavorite = false;
                    }

                    await _dataService.AddCartToDb(cartName, CartItems, GetCartItemsCount(CartItems),
                        GetCartPrice(CartItems));

                    _itemService.OnCartTbUpdated(EventArgs.Empty);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Klaida!", "Krepšelis tuščias!", "OK");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to save cart: {ex.Message}");
            }
        }


        [RelayCommand]
        async void CheapestStore(object obj)
        {

                if (IsBusy)
                {
                    return;
                }
            try
            {
                isBusy = true;
                var items = await _itemService.GetCheapestItems(CartItems);
                double totalPrice = 0;
                foreach (var item in items)
                {
                    item.Price = item.Price / 100;
                    item.PricePerUnitOfMeasure = item.PricePerUnitOfMeasure / 100;
                    totalPrice = item.Price * item.Quantity + totalPrice;
                }
                _itemService.OnCheapestCart(new CartUsedEventArgs(items));

                
                if (totalPrice > 0)
                {
                    await Shell.Current.GoToAsync(
                 $"{nameof(CheapestStorePage)}", new Dictionary<string, object> { { "TotalPrice", totalPrice } });
                }
                else {
                    await Shell.Current.GoToAsync("EmptyStorePage");
                }
                

            }

            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Klaida!", ex.Message, "OK");
                throw;
            }

            finally
            {
                isBusy = false;
            }

  
            
            
        }

        public double GetCartPrice(ObservableCollection<Item> cartItems)
        {
            double totalPrice = 0;
            foreach (Item I in cartItems)
            {
                totalPrice += I.Price * (double)I.Quantity;
            }

            return totalPrice;
        }

        public int GetCartItemsCount(ObservableCollection<Item> cartItems)
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
            if(item.Quantity > 1)
                item.Quantity--;
        }
      
    }
}
