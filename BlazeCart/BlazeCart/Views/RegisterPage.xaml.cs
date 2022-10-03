using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        this.BindingContext = new RegisterPageViewModel(this.Navigation);
    }
}