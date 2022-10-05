using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class ErrorPage : ContentPage
{
	public ErrorPage()
	{
		InitializeComponent();
        this.BindingContext = new ErrorPageViewModel(this.Navigation);
    }
}