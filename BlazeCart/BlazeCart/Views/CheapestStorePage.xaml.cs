using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CheapestStorePage : ContentPage
{
	public CheapestStorePage(CheapestStorePageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}