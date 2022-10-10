
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.ViewModels;
using Newtonsoft.Json;

namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage(ItemsViewModel itemsViewModel)
	{
		InitializeComponent();
        BindingContext = itemsViewModel;
    }

    public ItemCatalogPage()
    {
        InitializeComponent();
        BindingContext = new ItemsViewModel();
    }

   
}