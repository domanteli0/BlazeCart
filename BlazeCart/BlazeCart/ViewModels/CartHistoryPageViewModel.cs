
using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class CartHistoryPageViewModel : ObservableObject
    {
        public ObservableCollection<Cart> Carts { get; set; } = new();

        private CartService _cartService;

        [ObservableProperty] int cartItemCount;

        public CartHistoryPageViewModel(CartService cartService)
        {
            _cartService = cartService;
            Carts = _cartService.GetCarts();
            CartItemCount = Carts.Count;
        }

        



    }
}
