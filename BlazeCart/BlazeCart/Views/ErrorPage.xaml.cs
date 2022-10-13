using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class ErrorPage : ContentPage
{
	public ErrorPage(ErrorPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}