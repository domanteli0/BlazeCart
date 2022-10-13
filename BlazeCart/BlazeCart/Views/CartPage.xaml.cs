using BlazeCart.Services;
using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CartPage : ContentPage
{
	public CartPage(CartPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}