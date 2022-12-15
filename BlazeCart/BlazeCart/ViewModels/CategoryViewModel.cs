using System.Collections.ObjectModel;
using System.Diagnostics;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BlazeCart.ViewModels;

public partial class CategoryViewModel : BaseViewModel
{
    [ObservableProperty]
    public ObservableCollection<Category> categories = new();

    private readonly CategoryService _categoryService;

    private readonly ItemService _itemService;

    private int _startIndex = 0;

    [ObservableProperty]
    public bool isRefreshing;

    public CategoryViewModel(CategoryService categoryService, ItemService itemService)
    {
        _categoryService = categoryService;
        _itemService = itemService;
    }

    [RelayCommand]
    async void GetCategoriesAsync()
    {
        if (IsBusy)
        {
            return;
        }
        try
        {
            isBusy = true;

            //getting categories
            var categories = await _categoryService.GetCategories(_startIndex, 30);
            _startIndex += 20;
            
            //getting categories image
            foreach(var cat in categories)
            {
                var items = await _categoryService.GetItemsByCategoryId(cat.Id);
                cat.Count = items.Count();
                cat.Image = items[3].Image;
            }


            foreach (var category in categories)
                Categories.Add(category);
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
    async Task Tap(Category category)
    {
        await Shell.Current.GoToAsync($"{nameof(ItemCatalogPage)}", new Dictionary<string, object>
                  {
                      {"Id", category.Id},
                      {"NameLT", category.NameLT}
                      
                  });
    }
    [RelayCommand]
    void Refresh()
    {
        IsRefreshing = false;
    }

    [RelayCommand]
    async void Back()
    {
        await Shell.Current.GoToAsync("..");
    }

}


