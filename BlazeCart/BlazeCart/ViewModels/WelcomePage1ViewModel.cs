using BlazeCart.Views;
using System.Windows.Input;
namespace BlazeCart.ViewModels;

public class WelcomePage1ViewModel : ContentView
{
	public WelcomePage1ViewModel()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}