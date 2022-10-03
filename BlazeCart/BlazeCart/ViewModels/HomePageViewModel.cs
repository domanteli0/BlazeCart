using BlazeCart.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class HomePageViewModel
    {
        private INavigation _navigation;
        public ICommand SearchItemCommand { private set; get; }
        public ICommand CartHistoryCommand { private set; get; }
        public ICommand FavoriteItemsCommand { private set; get; }
        public HomePageViewModel(INavigation navigation)
        {
            SearchItemCommand = new Command(OnSearchItemCommand);
            CartHistoryCommand = new Command(OnCartHistoryCommand);
            FavoriteItemsCommand = new Command(OnFavoriteItemsCommand);
            _navigation = navigation;
        }

        async void OnSearchItemCommand(object obj)
        {
            await _navigation.PushModalAsync(new ItemCatalogPage());
        }

        async void OnCartHistoryCommand(object obj)
        {
            await _navigation.PushModalAsync(new ErrorPage());
        }
        async void OnFavoriteItemsCommand(object obj)
        {
            await _navigation.PushModalAsync(new ErrorPage());
        }
    }
}

