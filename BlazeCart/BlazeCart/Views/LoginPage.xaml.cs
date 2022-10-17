using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}