using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlazeCart.Views;

namespace BlazeCart.ViewModels
{
    internal class CheapestStorePageViewModel
    {
        private INavigation _navigation;

        public ICommand BackToCartCommand { private set; get; }

        public CheapestStorePageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            BackToCartCommand = new Command(OnBackToCartCommand);
        }

        async void OnBackToCartCommand(object obj)
        {
            await _navigation.PopModalAsync();
        }
    }
}
