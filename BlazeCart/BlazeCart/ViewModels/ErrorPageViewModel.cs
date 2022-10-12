using BlazeCart.Views;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class ErrorPageViewModel
    {
        public ICommand ReturnHomeCommand { private set; get; }
        public ErrorPageViewModel()
        {
            ReturnHomeCommand = new Command(OnReturnHomeCommand);
        }

        async void OnReturnHomeCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

    }
}
