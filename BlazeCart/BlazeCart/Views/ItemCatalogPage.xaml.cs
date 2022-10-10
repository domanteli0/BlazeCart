using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.ViewModels;
using Newtonsoft.Json;

namespace BlazeCart.Views;

public partial class ItemCatalogPage : ContentPage
{
	public ItemCatalogPage()
	{
		InitializeComponent();
        this.BindingContext = new ItemsViewModel(this.Navigation);
    }
}