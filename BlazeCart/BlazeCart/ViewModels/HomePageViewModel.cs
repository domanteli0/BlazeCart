using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {

        [RelayCommand]
        async void SearchItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ItemCatalogPage));
        }
        [RelayCommand]
        async void CartHistory(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CartHistoryPage));
        }
        [RelayCommand]
        async void FavoriteItems(object obj)
        {
            await Shell.Current.GoToAsync(nameof(FavoriteItemPage));
        }

        
    }
}

