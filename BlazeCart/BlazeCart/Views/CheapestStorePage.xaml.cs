using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CheapestStorePage : ContentPage
{
	public CheapestStorePage()
	{
		InitializeComponent();
        this.BindingContext = new CheapestStorePageViewModel(this.Navigation);
    }
}