using BlazeCart.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class ErrorPageViewModel
    {
        [RelayCommand]
        async void OnReturnHome(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

    }
}
