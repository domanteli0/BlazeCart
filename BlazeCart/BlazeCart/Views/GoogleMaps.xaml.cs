using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class GoogleMaps : ContentPage
{
	public GoogleMaps(GoogleMapsViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}
}