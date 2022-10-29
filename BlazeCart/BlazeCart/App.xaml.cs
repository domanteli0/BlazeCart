using BlazeCart.Views;

namespace BlazeCart;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
		//MainPage = new NavigationPage(new FavoriteItemPage());
	}
}
