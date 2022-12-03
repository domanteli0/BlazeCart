using System.Text.RegularExpressions;
using BlazeCart.Views;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;

namespace BlazeCart.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        [ObservableProperty] public string name;

        [ObservableProperty] public string surname;

        [ObservableProperty] public string email;

        [ObservableProperty] public string password;

        [ObservableProperty] public string confirmPassword;

        private readonly AuthService _authService;
        private async Task Register()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await _authService.RegisterAsync(email, password);
                await Shell.Current.GoToAsync(nameof(HomePage));
            }
            catch(FirebaseAuthException ex)
            {
                await Shell.Current.DisplayAlert("Klaida!","Neteisingi duomenys!", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public RegisterPageViewModel(AuthService authService)
        {
            _authService = authService;
        }

        [RelayCommand]
        async Task ValidateEntryFields(object obj)
        {
            string emailPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$";
            string passwordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";

            if (IsEmptyField())
            {
                await Shell.Current.DisplayAlert("Klaida!", "Užpildykite visus laukus!", "OK");
                return;
            }

            if (_matchPattern(emailPattern, Email).Success)
            {
                await Shell.Current.DisplayAlert("Klaida!", "Įveskite egzistuojantį el. pašto adresą.", "OK");
                return;
            }

            if (_matchPattern(passwordPattern, Password).Success)
            {
                await Shell.Current.DisplayAlert("Klaida!",
                    "Slaptažodis turi turėti bent 8 simbolius, viena didžiąją raidę, viena mažąją ir viena skaitmenį.",
                    "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Shell.Current.DisplayAlert("Klaida!", "Slaptažodžiai nesutampa.", "OK");
                return;
            }

            await Register();
        }

        private bool IsEmptyField()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email) ||
                   string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword);
        }

        readonly Func<string, string, Match> _matchPattern = (pattern, field) => Regex.Match(pattern, field);

    }
}
