using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class CheapestStorePageViewModel : ObservableObject
    {
        [RelayCommand]
        async void BackToCart(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
