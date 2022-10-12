using BlazeCart.Views;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class WelcomePage2ViewModel
    {
        public ICommand NextCommand { private set; get; }
        public WelcomePage2ViewModel()
        {
            NextCommand = new Command(OnNextCommand);
        }

        async void OnNextCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
