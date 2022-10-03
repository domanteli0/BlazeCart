using BlazeCart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class RegisterPageViewModel
    {
        private INavigation _navigation;
        public ICommand RegisterCommand { private set; get; }

        public RegisterPageViewModel(INavigation navigation)
        {
            RegisterCommand = new Command(OnRegisterCommand);
            _navigation = navigation;
        }

        async void OnRegisterCommand(object obj)
        {
            //here should be register page, but temporary home page
            await _navigation.PushModalAsync(new HomePage());
        }
    }
}
