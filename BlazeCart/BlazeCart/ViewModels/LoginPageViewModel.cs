using BlazeCart.Views;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class LoginPageViewModel
    {
        public ICommand LoginCommand { private set; get; }
        public ICommand RegisterCommand { private set; get; }

        public LoginPageViewModel()
        {
            LoginCommand = new Command(OnLoginCommand);
            RegisterCommand = new Command(OnRegisterCommand);
        }

        async void OnLoginCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

        async void OnRegisterCommand(object obj)
        {
            //here should be register page, but temporary home page
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
