using BlazeCart.ViewModels;
namespace BlazeCart.Views;

public partial class EmptyStorePage : ContentPage
{
	public EmptyStorePage(EmptyStorePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}