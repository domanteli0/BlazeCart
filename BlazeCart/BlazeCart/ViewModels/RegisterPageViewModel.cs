using System.Text.RegularExpressions;
using BlazeCart.Models;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        [ObservableProperty] public string name;

        [ObservableProperty] public string surname;

        [ObservableProperty] public string email;

        [ObservableProperty] public string password;

        [ObservableProperty] public string confirmPassword;

        private User _user = new User();
        async Task Register()
        {
                this._user.Name = name;
                this._user.Surname = surname;
                this._user.Email = email;
                this._user.Password = password;
                await Shell.Current.GoToAsync(nameof(HomePage));
        }

        [RelayCommand]
        async Task ValidateEntryFields(object obj)
        {
            string emailPattern = @"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$";
            string passwordPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";

            if (IsEmptyField())
            {
                await AppShell.Current.DisplayAlert("Klaida!", "Įveskite vardą ir pavardę", "OK");
                return;
            }

            if (!MatchPattern(emailPattern, Email).Success)
            {
                await AppShell.Current.DisplayAlert("Klaida!", "Įveskite egzistuojantį el. pašto adresą.", "OK");
                return;
            }
            if (!MatchPattern(passwordPattern, Password).Success)
            {
                await AppShell.Current.DisplayAlert("Klaida!",
                    "Slaptažodis turi turėti bent 8 simbolius, viena didžiąją raidę, viena mažąją ir viena skaitmenį.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await AppShell.Current.DisplayAlert("Klaida!", "Slaptažodžiai nesutampa.", "OK");
                return;
            }
            await Register();
        }

        private bool IsEmptyField()
        {
            return string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email) ||
                   string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword);
        }

        private  Match MatchPattern(string pattern, string field)
        {
            //lambda?
            return Regex.Match(field, pattern);
        }

    }
}
