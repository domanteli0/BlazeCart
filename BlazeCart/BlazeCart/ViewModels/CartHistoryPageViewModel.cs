using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BlazeCart.ViewModels
{
    public partial class CartHistoryPageViewModel : BaseViewModel
    {
        public ObservableCollection<Cart> Carts { get; set; }

        private CartService _cartService;

        [ObservableProperty] int cartTotalPrice;

        public  CartHistoryPageViewModel(CartService cartService)
        {
            Carts = new ObservableCollection<Cart>();
            _cartService = cartService;
            Task.Run(() => this.Refresh()).Wait();
        }

        public async Task Refresh()
        {
            IsBusy = true;

            await Task.Delay(100);

            Carts.Clear();

            var carts = await _cartService.GetCartsFromDb();

            foreach (var cart in carts)
            {
                
                Carts.Add(cart);
            }
            isBusy = false;
        }

        [RelayCommand]
        async Task Remove(Cart cart)
        {
            await _cartService.RemoveCartFromDb(cart.Id);
            await Refresh();
        }
    }
}
