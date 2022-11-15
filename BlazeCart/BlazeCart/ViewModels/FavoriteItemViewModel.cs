using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels;

public partial class FavoriteItemViewModel : ObservableObject
{
    public ObservableCollection<Item> FavoriteItems { get; set; }
    public bool IsBusy { get; set; }
    private readonly DataService _dataService;

    private readonly ItemService _itemService;

    public FavoriteItemViewModel(DataService dataService, ItemService itemService)
    {
        _dataService = dataService;
        _itemService = itemService;
        _itemService.FavTbUpdated += FavTbUpdatedEventHandler;
        FavoriteItems = new ObservableCollection<Item>();
        Task.Run(() => this.Refresh()).Wait();
    }

    private void FavTbUpdatedEventHandler(object sender, EventArgs e)
    {
        Task.Run(() => this.Refresh()).Wait();
    }
    public async Task Refresh()
    {
        IsBusy = true;

        await Task.Delay(100);

        if (FavoriteItems.Count > 0)
        {
            FavoriteItems.Clear();
        }

        var favItems = await _dataService.GetFavoriteItemsFromDb();
        foreach (var item in favItems)
        {

            FavoriteItems.Add(item);
        }
        IsBusy = false;

    }

    [RelayCommand]
    async void Cart(Item item)
    {
        _itemService.AddToCart(item);
        await Shell.Current.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");

    }

    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    async Task Remove(Item item)
    {
        await _dataService.RemoveFavoriteItemFromDb(item.ItemId);
        await Refresh();
    }
}

