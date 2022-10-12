using BlazeCart.Views;
using System.Windows.Input;
namespace BlazeCart.ViewModels;

public class WelcomePage1ViewModel
{
    public ICommand NextCommand { private set; get; }

    public ICommand SkipCommand { private set; get; }
    public WelcomePage1ViewModel()
    {
        NextCommand = new Command(OnNextCommand);
        SkipCommand = new Command(OnSkipCommand);
    }

    async void OnNextCommand(object obj)
    {
        await Shell.Current.GoToAsync(nameof(WelcomePage2));
    }

    async void OnSkipCommand(object obj)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}