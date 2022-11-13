using CommunityToolkit.Mvvm.ComponentModel;
using BlazeCart.Models;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    [QueryProperty(nameof(Item), "Item")]
    [QueryProperty(nameof(Name), "Name")]
    [QueryProperty(nameof(Price), "Price")]
    [QueryProperty(nameof(Image), "Image")]
    [QueryProperty(nameof(Description), "Description")]

    public partial class ItemPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Item item;
        [ObservableProperty]
        public new string name;

        [ObservableProperty]
        public double price;

        [ObservableProperty]
        public Uri image;

        [ObservableProperty]
        public string description;

        private CartPageViewModel _vm;

        public ItemPageViewModel(CartPageViewModel vm)
        {
            _vm = vm;
        }

        [RelayCommand]
        async void Cart(object obj)
        {
            _vm.CartItems.Add(item);
            await Shell.Current.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");
        }
       
    }
}
