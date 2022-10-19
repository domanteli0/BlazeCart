using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class ItemPage : ContentPage
{
	public ItemPage(ItemPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}