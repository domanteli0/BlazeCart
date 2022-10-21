using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CartHistoryPage : ContentPage
{
	public CartHistoryPage(CartHistoryPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}