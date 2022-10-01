using BlazeCart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class WelcomePage2ViewModel
    {
        private INavigation _navigation;
        public ICommand NextCommand { private set; get; }
        public WelcomePage2ViewModel(INavigation navigation)
        {
            NextCommand = new Command(OnNextCommand);
            _navigation = navigation;
        }

        async void OnNextCommand(object obj)
        {
            await _navigation.PushModalAsync(new LoginPage());
        }
    }
}
