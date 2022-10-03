using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        this.BindingContext = new HomePageViewModel(this.Navigation);
    }
}