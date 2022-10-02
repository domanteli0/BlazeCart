using BlazeCart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class LoginPageViewModel
    {
        private INavigation _navigation;
        public ICommand LoginCommand { private set; get; }
        public ICommand RegisterCommand { private set; get; }

        public LoginPageViewModel(INavigation navigation)
        {
            LoginCommand = new Command(OnLoginCommand);
            RegisterCommand = new Command(OnRegisterCommand);
            _navigation = navigation;
        }

        async void OnLoginCommand(object obj)
        {
            await _navigation.PushModalAsync(new HomePage());
        }

        async void OnRegisterCommand(object obj)
        {
            //here should be register page, but temporary home page
            await _navigation.PushModalAsync(new HomePage());
        }
    }
}
