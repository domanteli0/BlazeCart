using BlazeCart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class ErrorPageViewModel
    {
        private INavigation _navigation;
        public ICommand ReturnHomeCommand { private set; get; }
        public ErrorPageViewModel(INavigation navigation)
        {
            ReturnHomeCommand = new Command(OnReturnHomeCommand);
            _navigation = navigation;
        }

        async void OnReturnHomeCommand(object obj)
        {
            await _navigation.PushModalAsync(new HomePage());
        }

    }
}
