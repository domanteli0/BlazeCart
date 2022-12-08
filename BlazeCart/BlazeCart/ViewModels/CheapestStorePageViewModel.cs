using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace BlazeCart.ViewModels
{
    [QueryProperty(nameof(TotalPrice), "TotalPrice")]
    public partial class CheapestStorePageViewModel : BaseViewModel
    {

        public ObservableCollection<Item> CartItems { get; set; } = new();
        private ItemService _itemService;
        [ObservableProperty] double totalPrice;
        public CheapestStorePageViewModel(ItemService itemservice) {
            _itemService = itemservice;
            _itemService.CheapestCart += CheapestCartEventHandler;


        }

        private void CheapestCartEventHandler(object sender, CartUsedEventArgs e)
        {
            CartItems.Clear();
            foreach (var item in e.Items)
            {
                CartItems.Add(item);
                totalPrice = item.Price + totalPrice;
            }
            
        }



        [RelayCommand]
        async void BackToCart(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
