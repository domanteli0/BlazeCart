using BlazeCart.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels
{
    public partial class WelcomePage2ViewModel
    {

        [RelayCommand]
        async void Next(object obj)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
