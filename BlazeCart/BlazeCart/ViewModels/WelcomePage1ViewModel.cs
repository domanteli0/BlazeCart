using BlazeCart.Views;
using System.Windows.Input;
namespace BlazeCart.ViewModels;

public class WelcomePage1ViewModel
{
    private INavigation _navigation;
    public ICommand NextCommand { private set; get; }
	public WelcomePage1ViewModel(INavigation navigation)
    {
        NextCommand = new Command(OnNextCommand);
		_navigation = navigation;
	}

    async void OnNextCommand(object obj)
    {
        await _navigation.PushModalAsync(new WelcomePage2());
    }
}