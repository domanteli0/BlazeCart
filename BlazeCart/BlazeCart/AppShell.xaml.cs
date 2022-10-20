using BlazeCart.Views;

namespace BlazeCart;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ItemCatalogPage), typeof(ItemCatalogPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ErrorPage), typeof(ErrorPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
        Routing.RegisterRoute(nameof(WelcomePage1), typeof(WelcomePage1));
        Routing.RegisterRoute(nameof(WelcomePage2), typeof(WelcomePage2));
        Routing.RegisterRoute(nameof(CheapestStorePage), typeof(CheapestStorePage));
        Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
        Routing.RegisterRoute(nameof(CartHistoryPage), typeof(CartHistoryPage));
    }
}
