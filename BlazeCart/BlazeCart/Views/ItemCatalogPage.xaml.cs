using Android.App.AppSearch;
using BlazeCart.Models;
using BlazeCart.Services;
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


    private void OnValueChangeEnd(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnLabelCreated(object sender, SliderLabelCreatedEventArgs e)
    {
        throw new NotImplementedException();
    }
}
