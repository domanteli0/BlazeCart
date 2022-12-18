using System.Collections.ObjectModel;
using System.Diagnostics;
using BlazeCart.Models;
using BlazeCart.Services;
using BlazeCart.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyCache.FileStore;

namespace BlazeCart.ViewModels;

public partial class CategoryViewModel : BaseViewModel
{
    [ObservableProperty]
    public ObservableCollection<Category> categories = new();

    private readonly CategoryService _categoryService;

    private readonly ItemService _itemService;

    private int _startIndex = 0;

    private bool flag = true;
   

    [ObservableProperty]
    public bool isRefreshing;

    public CategoryViewModel(CategoryService categoryService, ItemService itemService)
    {
        _categoryService = categoryService;
        _itemService = itemService;
       Barrel.Current.EmptyAll();

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
            int count = await _categoryService.GetCategoriesCount();
            
            if (_startIndex + 30 <= count)
            {
                var categories = await _categoryService.GetCategories(_startIndex, 30);
                _startIndex += 30;

                foreach (var cat in categories)
                {
                    var items = await _categoryService.GetItemsByCategoryId(cat.Id);
                    cat.Count = items.Count();
                    if(cat.Count > 0)
                        cat.Image = items[0].Image;
                    
                }

                foreach (var category in categories)
                {
                    if (category.Count > 0)
                    {
                        Categories.Add(category);
                    }
                }
            }
            else if(_startIndex < count)
            {
                var categories = await _categoryService.GetCategories(_startIndex, count - _startIndex);

                foreach (var cat in categories)
                {
                    var items = await _categoryService.GetItemsByCategoryId(cat.Id);
                    cat.Count = items.Count();
                    if(cat.Count > 0)
                        cat.Image = items[0].Image;
                   
                }


                foreach (var category in categories)
                {
                    if (category.Count > 0)
                    {
                        Categories.Add(category);
                    }
                }
                _startIndex = count;
                    
            }
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
                      {"NameLT", category.NameLT},
                      {"Count", category.Count }
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


