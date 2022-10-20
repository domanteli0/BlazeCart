using BlazeCart.ViewModels;
using Syncfusion.Maui.Sliders;
using SelectionChangedEventArgs = Syncfusion.Maui.Inputs.SelectionChangedEventArgs;

namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage(ItemsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}
