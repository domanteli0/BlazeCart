using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class WelcomePage2 : ContentPage
{
	public WelcomePage2()
	{
		InitializeComponent();
        this.BindingContext = new WelcomePage2ViewModel();
    }
}