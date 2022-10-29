using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels;

public partial class FavoriteItemViewModel : ObservableObject
{
    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}

