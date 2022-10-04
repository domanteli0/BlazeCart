using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class HomePage : TabbedPage
{
	public HomePage()
	{
		InitializeComponent();
        this.BindingContext = new HomePageViewModel(this.Navigation);
    }
}