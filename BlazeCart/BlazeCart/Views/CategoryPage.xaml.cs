using BlazeCart.ViewModels;

namespace BlazeCart.Views;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(CategoryViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}