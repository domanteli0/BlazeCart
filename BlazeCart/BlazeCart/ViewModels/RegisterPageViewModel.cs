using System.ComponentModel.DataAnnotations;
using BlazeCart.Views;
using System.Windows.Input;
using BlazeCart.Models;

namespace BlazeCart.ViewModels
{
    internal class RegisterPageViewModel
    {
        private INavigation _navigation;
        public ICommand RegisterCommand { private set; get; }
        private User _user = new User();

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$", ErrorMessage = "Name must start with a capital letter and contain only letters.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^\p{Lu}[ \p{L}'-]*[\p{Ll}]$", ErrorMessage = "Surname must start with a capital letter and contain only letters.")]
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$", ErrorMessage = "Password should have at least 8 characters, one uppercase letter, one lowercase letter and a digit.")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterPageViewModel()
        {
            RegisterCommand = new Command(OnRegisterCommand);
        }

        async void OnRegisterCommand(object obj)
        {
            this._user.Name = Name;
            this._user.Surname = Surname;
            this._user.Email = Email;
            this._user.Password = Password;
            //here should be register page, but temporary home page
            await Shell.Current.GoToAsync(nameof(HomePage));
        }
    }
}
