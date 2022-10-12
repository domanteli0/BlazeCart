using BlazeCart.Views;
using System.Windows.Input;

namespace BlazeCart.ViewModels
{
    internal class HomePageViewModel
    {
        public ICommand SearchItemCommand { private set; get; }
        public ICommand CartHistoryCommand { private set; get; }
        public ICommand FavoriteItemsCommand { private set; get; }
        public HomePageViewModel()
        {
            SearchItemCommand = new Command(OnSearchItemCommand);
            CartHistoryCommand = new Command(OnCartHistoryCommand);
            FavoriteItemsCommand = new Command(OnFavoriteItemsCommand);
        }

        async void OnSearchItemCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ItemCatalogPage));
        }

        async void OnCartHistoryCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ErrorPage));
        }
        async void OnFavoriteItemsCommand(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ErrorPage));
        }
    }
}

