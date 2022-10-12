using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class WelcomePage1 : ContentPage
{
	public WelcomePage1()
	{
		InitializeComponent();
        this.BindingContext = new WelcomePage1ViewModel();
    }
}