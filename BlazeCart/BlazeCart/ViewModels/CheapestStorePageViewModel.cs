using BlazeCart.Views;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class CheapestStorePageViewModel
    {
        public ICommand BackToCartCommand { private set; get; }

        public CheapestStorePageViewModel()
        {
            BackToCartCommand = new Command(OnBackToCartCommand);
        }

        async void OnBackToCartCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CartPage));
        }
    }
}
