using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;


namespace BlazeCart.ViewModels
{
    public partial class CartHistoryPageViewModel : BaseViewModel
    {
        public ObservableCollection<Cart> Carts { get; set; }

        private DataService _dataService;

        private ItemService _itemService;

        [ObservableProperty] int cartTotalPrice;

        private readonly ILogger<CartHistoryPageViewModel> _logger;

        public  CartHistoryPageViewModel(DataService dataService, ItemService itemService, ILogger<CartHistoryPageViewModel> logger)
        {
            Carts = new ObservableCollection<Cart>();
            _dataService = dataService;
            _itemService = itemService;
            _itemService.CartTbUpdated += CartTbUpdatedEventHandler;
            _logger = logger;
            Task.Run(this.Refresh).Wait();
        }

        private void CartTbUpdatedEventHandler(object sender, EventArgs e)
        {
            Task.Run(this.Refresh).Wait();
            _logger.LogInformation("Handled event CartTbUpdated");
        }
        public async Task Refresh()
        {
            IsBusy = true;

            try
            {
                await Task.Delay(100);
                Carts.Clear();
                var carts = await _dataService.GetCartsFromDb();
                foreach (var cart in carts)
                {

                    Carts.Add(cart);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable get carts from DB, {ex.Message}");
                throw;
            }

            IsBusy = false;
        }

        [RelayCommand]
        async Task UseCart(Cart cart)
        {
            try
            {
                _itemService.PutItems(cart.CartItems);
                _logger.LogInformation($"Successfully used {cart.Id}, {cart.Name}");
                await Shell.Current.DisplayAlert("Pritaikyta!", "Krepšelis sėkmingai pritaikytas!", "OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to use {cart.Id}, {cart.Name}, error: {ex.Message}");
                throw;
            }
        }

        [RelayCommand]
        async Task Remove(Cart cart)
        {
            try
            {
                await _dataService.RemoveCartFromDb(cart.Id);
                await Refresh();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to remove {cart.Id},{cart.Name} from DB, error: {ex.Message}");
                throw;
            }
            
        }
    }
}
