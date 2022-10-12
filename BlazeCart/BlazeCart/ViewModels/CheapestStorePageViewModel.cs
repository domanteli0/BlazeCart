using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class CheapestStorePageViewModel
    {
        [RelayCommand]
        async void BackToCart(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
