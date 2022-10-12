using System.ComponentModel.DataAnnotations;
using BlazeCart.Models;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class RegisterPageViewModel
    {
        private User _user = new User();

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Name must start with a capital letter and contain only letters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$",
            ErrorMessage = "Surname must start with a capital letter and contain only letters.")]
        public string Surname { get; set; }

        [EmailAddress] public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$",
            ErrorMessage =
                "Password should have at least 8 characters, one uppercase letter, one lowercase letter and a digit.")]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        [RelayCommand]
        async Task Register(object obj)
        {
            this._user.Name = Name;
            this._user.Surname = Surname;
            this._user.Email = Email;
            this._user.Password = Password;
           // await Shell.Current.DisplayAlert(Name, Email, Password, Surname);
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
