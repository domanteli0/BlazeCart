using System.Collections.ObjectModel;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels;

public partial class FavoriteItemViewModel : ObservableObject

{
    public ObservableCollection<Item> FavoriteItems { get; set; } = new();
    public bool IsRefresh { get; set; }
    public bool IsBusy { get; set; }
    private readonly DataService _dataService;

    public FavoriteItemViewModel(DataService dataService)
    {
        _dataService = dataService;
        GetItemsFromDb();
    }

    private async void GetItemsFromDb()
    {
        IsBusy = true;

        if (this.FavoriteItems.Count > 0)
        {
            FavoriteItems.Clear();
        }

        var favItems = await _dataService.GetFavoriteItemsFromDb();
        FavoriteItems = new ObservableCollection<Item>(favItems.ToList());
        IsBusy = false;
    }


    [RelayCommand]
    async void Refresh()
    {
        IsRefresh = true;

        if (FavoriteItems.Count > 0)
        {
            FavoriteItems.Clear();
        }

        var favItems = await _dataService.GetFavoriteItemsFromDb();
        FavoriteItems = new ObservableCollection<Item>(favItems.ToList());
        IsRefresh = false;

    }

    [RelayCommand]
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}

