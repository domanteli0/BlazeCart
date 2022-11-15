using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;

namespace BlazeCart.ViewModels;


public partial class ItemsViewModel : BaseViewModel
{
    [ObservableProperty]
    public bool isRefreshing;
    public ObservableCollection<Item> Items { get; set; } = new();

    [ObservableProperty]
    private ObservableCollection<Item> cartItems = new();

    [ObservableProperty]  double maximum;
    [ObservableProperty]  double minimum;
    [ObservableProperty]  double interval;
    [ObservableProperty]  bool isVisible;
    [ObservableProperty]  double rangeStart;
    [ObservableProperty] double rangeEnd;

    public ObservableCollection<String> ComboBoxCommands { get; set; }
    [ObservableProperty] string selectedCommand;

    private readonly ItemService _itemService;
    private readonly ItemSearchBarService _itemSearchBarService;
    private readonly SliderService _sliderService;
    private ItemFilterService _itemFilterService;
    private readonly DataService _dataService;

    [ObservableProperty]
    public ObservableCollection<Item> searchResults = new();

    public ItemsViewModel(ItemService itemService, ItemSearchBarService itemSearchBarService, SliderService sliderService, ItemFilterService itemFilterService, DataService dataService)
    {
        ComboBoxCommands = new ObservableCollection<string>
        {
            "Abėcėlę (A-Ž)",
            "Abėcėlę (Ž-A)",
            "Kainą nuo mažiausios",
            "Kainą nuo didžiausios",
            "Kainą nuo maž. (už mato vnt.)",
            "Kainą nuo didž. (už mato vnt.)"
        };

        _itemService = itemService;
        _itemSearchBarService = itemSearchBarService;
        _sliderService = sliderService;
        _itemFilterService = itemFilterService;
        _dataService = dataService;
        GetItemsAsync();
        SearchResults = Items;
        LoadSlider();
        _itemFilterService = itemFilterService;
    
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
            await Shell.Current.DisplayAlert("Klaida!", ex.Message, "OK");
        }

        finally
        {
            isBusy = false;
        }

    }

    [RelayCommand]
    void SelectionChanged()
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    async void AddItemToFavorites(Item item)
    {
        try
        {
            item.IsFavorite = true;
            await Shell.Current.DisplayAlert("Prekės pridėjimas sėkmingas", "Sėkmingai pažymėjote prekę kaip mėgstamiausią", "OK");
            await _dataService.AddFavoriteItemToDb(item);
            _itemService.OnFavTbUpdated(EventArgs.Empty);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Klaida!", ex.Message, "OK");
        }
       
    }

    [RelayCommand]
    async void LoadSlider()
    {
        if (IsBusy)
        {
            return;
        }
        try
        {
            isBusy = true;
            if (SearchResults.Count <= 1)
            {
                IsVisible = false;
            }

            else
            {
                IsVisible = true;
                Maximum = _sliderService.GetMaximum(SearchResults);
                Minimum = _sliderService.GetMinimum(SearchResults);
                RangeStart = Minimum;
                RangeEnd = Maximum;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to update the slider: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            isBusy = false;
        }
    }

    [RelayCommand]
     async void DragCompleted()
     {
         if (IsBusy)
         {
             return;
         }
         try
         {
             isBusy = true;
             if (double.IsNaN(rangeStart) || double.IsNaN(rangeEnd))
             {
                 Debug.WriteLine($"Range selected is incorrect");
                 isBusy = false;
                 return;
             }
            _sliderService.FetchItems(Items);
            SearchResults = _sliderService.GetSearchResults(rangeStart,rangeEnd);
            LoadSlider();
         }
         catch (Exception ex)
         {
            Debug.WriteLine($"Slider error: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
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
        {
            SearchResults = Items;
            _sliderService.GetSearchResults(rangeStart, rangeEnd);
            LoadSlider();
        }
        else
        {
            SearchResults = _itemSearchBarService.GetSearchResults(query);
            _sliderService.GetSearchResults(rangeStart, rangeEnd);
            LoadSlider();

        }

    }


    [RelayCommand]
    static async void Back(object obj)
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async void Cart(Item item)
    {
        _itemService.AddToCart(item);
        await Shell.Current.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");
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
        LoadSlider();
    }
}
