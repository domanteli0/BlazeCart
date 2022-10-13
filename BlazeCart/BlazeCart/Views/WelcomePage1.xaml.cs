using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class WelcomePage1 : ContentPage
{
	public WelcomePage1(WelcomePage1ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}