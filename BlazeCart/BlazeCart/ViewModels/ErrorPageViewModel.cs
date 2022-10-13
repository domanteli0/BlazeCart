using BlazeCart.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class ErrorPageViewModel : ObservableObject
    {
        [RelayCommand]
        async void OnReturnHome(object obj)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

    }
}
