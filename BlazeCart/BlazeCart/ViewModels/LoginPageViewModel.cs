using BlazeCart.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    partial class LoginPageViewModel
    {
        [RelayCommand]
        async void Login(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

        [RelayCommand]
        async void Register(object obj)
        {
            //here should be register page, but temporary home page
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
