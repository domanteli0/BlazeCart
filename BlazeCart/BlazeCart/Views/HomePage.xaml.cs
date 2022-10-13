using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}