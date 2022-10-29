using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class FavoriteItemPage : ContentPage
{
	public FavoriteItemPage(FavoriteItemViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}