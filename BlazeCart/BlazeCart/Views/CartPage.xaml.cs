using BlazeCart.Services;
using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CartPage : ContentPage
{
	public CartPage()
	{
		InitializeComponent();
        this.BindingContext = new CartPageViewModel(this.Navigation);
    }
}