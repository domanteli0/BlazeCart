using Android.App.AppSearch;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.ViewModels;
namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage(ItemsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    
}
