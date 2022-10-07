using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BlazeCart.Views;

namespace BlazeCart.ViewModels
{
    internal class CartPageViewModel
    {
        public ICommand RemoveCommand { get; set; }

        public INavigation _navigation;
        public CartPageViewModel(INavigation _navigation)
        {
            RemoveCommand = new Command(OnRemoveCommand);
        }

        async void OnRemoveCommand(object obj)
        {
            //remove an item from current cart

            //and then refresh page
           await _navigation.PushModalAsync(new CartPage());
        }
    }
}
