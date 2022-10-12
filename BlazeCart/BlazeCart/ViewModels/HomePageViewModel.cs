using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class HomePageViewModel
    {
        [RelayCommand]
        async void SearchItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ItemCatalogPage));
        }
        [RelayCommand]
        async void CartHistory(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ErrorPage));
        }
        [RelayCommand]
        async void FavoriteItems(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ErrorPage));
        }
    }
}

