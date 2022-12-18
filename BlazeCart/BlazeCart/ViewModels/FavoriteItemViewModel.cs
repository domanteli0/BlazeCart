using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace BlazeCart.ViewModels;

public partial class FavoriteItemViewModel : ObservableObject
{
    public ObservableCollection<Item> FavoriteItems { get; set; }
    public bool IsBusy { get; set; }
    private readonly DataService _dataService;

    private readonly ItemService _itemService;

    private ILogger<FavoriteItemViewModel> _logger;
    public FavoriteItemViewModel(DataService dataService, ItemService itemService, ILogger<FavoriteItemViewModel> logger)
    {
        _dataService = dataService;
        _itemService = itemService;
        _logger = logger;
        _itemService.FavTbUpdated += FavTbUpdatedEventHandler;
        FavoriteItems = new ObservableCollection<Item>();
        Task.Run(this.Refresh).Wait();
    }

    private void FavTbUpdatedEventHandler(object sender, EventArgs e)
    {
        Task.Run(this.Refresh).Wait();
        _logger.LogInformation("Handled event FavTbUpdated");
    }
    public async Task Refresh()
    {

        try
        {
            IsBusy = true;
            await Task.Delay(100);

            if (FavoriteItems.Count > 0)
            {
                FavoriteItems.Clear();
            }

            var favItems = await _dataService.GetFavoriteItemsFromDb();
            foreach (var item in favItems)
                FavoriteItems.Add(item);

            _logger.LogInformation("Successfully retrieved items from .json");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unable to get favorite items from .json: {ex.Message} ");
            throw;
        }
        finally
        {
            IsBusy = false;
        }
        
    }

    [RelayCommand]
    async void Cart(Item item)
    {
        try
        {
            _itemService.AddToCart(item);
            _logger.LogInformation($"Successfully added item to cart: {item.ItemId}, {item.NameLT}");
            await Shell.Current.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add favorite item to cart: {item.ItemId}, {item.NameLT} | {ex.Message}");
            throw;
        }

    }

    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Remove(Item item)
    {
        try
        {
            await _dataService.RemoveFavoriteItemFromDb(item.ItemId);
            await Refresh();
            _logger.LogInformation($"Successfully removed favorite item  from DB: {item.ItemId}, {item.NameLT}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to remove favorite item from DB: {item.ItemId}, {item.NameLT} | {ex.Message}");
            throw;
        }
    }
}

