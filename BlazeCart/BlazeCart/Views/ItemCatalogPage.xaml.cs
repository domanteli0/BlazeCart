using BlazeCart.ViewModels;
using Syncfusion.Maui.Sliders;

namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage(ItemsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }


}
