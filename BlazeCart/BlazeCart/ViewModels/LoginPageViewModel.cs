using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;

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
