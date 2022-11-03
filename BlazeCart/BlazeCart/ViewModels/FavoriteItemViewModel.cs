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
    public bool IsRefresh { get; set; }
    public bool IsBusy { get; set; }
    private readonly DataService _dataService;


    public FavoriteItemViewModel(DataService dataService)
    {
        _dataService = dataService;
        FavoriteItems = new ObservableCollection<Item>();
        Task.Run(() => this.Refresh()).Wait();
    }


    public async Task Refresh()
    {
        IsBusy = true;

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
    async void Back(object obj)
    {
        await Shell.Current.GoToAsync(nameof(HomePage));
    }
}

