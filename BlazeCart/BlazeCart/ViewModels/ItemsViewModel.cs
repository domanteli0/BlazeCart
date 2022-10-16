using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

//Inserting service
namespace BlazeCart.ViewModels;


public partial class ItemsViewModel : BaseViewModel
{
    [ObservableProperty]
    public bool isRefreshing;
    public ObservableCollection<Item> Items { get; set; } = new();

    private CartPageViewModel _vm;

    [ObservableProperty]
    private ObservableCollection<Item> cartItems = new();

    private int cartItemcount;


    private ItemService _itemService;
    private ItemSearchBarService _itemSearchBarService;

    [ObservableProperty]
    public ObservableCollection<Item> searchResults = new();
    private Cart cart;

    public ItemsViewModel(ItemService itemService, CartPageViewModel vm, ItemSearchBarService itemSearchBarService)
    {
        _itemService = itemService;
        _itemSearchBarService = itemSearchBarService;
        _vm = vm;
        GetItemsAsync();
        SearchResults = Items;
    }

    async void GetItemsAsync()
    {

        if (IsBusy)
        {
            return;
        }
        try
        {
            isBusy = true;
            var items = await _itemService.GetItems();

            if (Items.Count != 0)
            {
                Items.Clear();
            }

            foreach (var item in items)
                Items.Add(item);
        }

        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get items: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }

        finally
        {
            isBusy = false;
        }

    }


    [RelayCommand]
    void PerformSearch(string query)
    {
        _itemSearchBarService.FetchItems(Items);
        if (query == null)
            SearchResults = Items;
        SearchResults = _itemSearchBarService.GetSearchResults(query);
        //Items = SearchResults;
    }


    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async void Cart(Item item)
    {
        _vm.CartItems.Add(item);
        await Application.Current.MainPage.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");
    }

    [RelayCommand]
    async Task Tap(Item item)
    {
        if(item!=null)
        {
            await Shell.Current.GoToAsync(
                  $"{nameof(ItemPage)}", new Dictionary<string, object>
                  { 
                      {"Item", item},
                      {"Name", item.Name},
                       {"Price", item.Price},
                       {"Image", item.Image},
                      {"Description", item.Description}
                  });
            
        }
        else
        {
           await Shell.Current.DisplayAlert("Klaida!", "Nepavyko atidaryti prekės informaciją!", "OK");
        }
    }

    [RelayCommand]
    void Refresh()
    {
        SearchResults = Items;
        IsRefreshing = false;
    }
}
