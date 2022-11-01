using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class FavoriteItemPage : ContentPage
{
	public FavoriteItemPage(FavoriteItemViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}