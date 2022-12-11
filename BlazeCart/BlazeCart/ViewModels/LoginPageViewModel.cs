using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
namespace BlazeCart.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;
        private readonly AuthService _authService;
        public LoginPageViewModel(AuthService authService)
        {
            _authService = authService;
        }
        [RelayCommand]
        async Task Login(object obj)
        {
            if (IsBusy)
                return;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                await Shell.Current.DisplayAlert("Error!", "Please fill all fields.", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                await _authService.LoginAsync(email, password);

                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch (FirebaseAuthException ex)
            {
                await Shell.Current.DisplayAlert("Klaida!","Neteisingi duomenys!", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async void Register(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}
