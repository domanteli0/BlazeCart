using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        this.BindingContext = new LoginPageViewModel();
    }
}