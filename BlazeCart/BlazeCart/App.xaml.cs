namespace BlazeCart;
using BlazeCart.Views;
using BlazeCart.Resources;
using System.Reflection;


public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		//MainPage = new AppShell();
		MainPage = new NavigationPage(new WelcomePage1());
	}
}
