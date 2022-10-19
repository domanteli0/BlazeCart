using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class WelcomePage2 : ContentPage
{
	public WelcomePage2(WelcomePage2ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}