using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Android.App.AppSearch;

//Inserting service
namespace BlazeCart.ViewModels;


public partial class ItemsViewModel : BaseViewModel
{
    public ObservableCollection<Item> Items { get; set; } = new();

    public ObservableCollection<Item> CartItems { get; set; } = new();

    private ObservableCollection<Item> _searchResults;

    public Item SelectedItem { get; set; }

    ItemService _itemService = new();
    

    public ItemsViewModel()
    {
        Items = new ObservableCollection<Item>();
        GetItemsAsync();
        _searchResults = Items;
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


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public ICommand PerformSearch => new Command<string>((string query) =>
    {
        ItemSearchBarService _searchBarService = new(Items);
        SearchResults = _searchBarService.GetSearchResults(query);
        //Items = SearchResults;
    });

    
    
    
    public ObservableCollection<Item> SearchResults
    {
        get => _searchResults;
        set
        {
            _searchResults = value;
            NotifyPropertyChanged();
        }
    }

    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async void Cart(Item item)
    {
        this.SelectedItem = item;
        this.CartItems.Add(SelectedItem);
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


}
