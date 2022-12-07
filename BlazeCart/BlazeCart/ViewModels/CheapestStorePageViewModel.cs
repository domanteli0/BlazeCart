using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BlazeCart.ViewModels
{
    public partial class CheapestStorePageViewModel : ObservableObject
    {

        public ObservableCollection<Item> CartItems { get; set; }
        private ItemService _itemService;

        public CheapestStorePageViewModel(ItemService itemservice) {
            this._itemService = itemservice;
            CartItems = _itemService.GetItems().Result;

        }



        [RelayCommand]
        async void BackToCart(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
