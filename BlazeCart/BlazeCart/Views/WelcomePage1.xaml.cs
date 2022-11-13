using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class WelcomePage1 : ContentPage
{
	public WelcomePage1(WelcomePage1ViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    async void ImageButton_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(WelcomePage2));
    }

    async void Button_OnClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
}