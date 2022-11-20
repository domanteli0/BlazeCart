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
    public ObservableCollection<Category> Categories { get; set; } = new();
    private Stack<Collection<Category>> _pastCategories { get; set; } = new();
    private readonly CategoryService _categoryService;

    [ObservableProperty]
    public bool isRefreshing;

    public CategoryViewModel(CategoryService categoryService)
    {
        _categoryService = categoryService;
        GetCategoriesAsync();
    }

    async void GetCategoriesAsync()
    {
        if (IsBusy)
        {
            return;
        }
        try
        {
            isBusy = true;
            var categories = await _categoryService.GetCategories();
            _pastCategories.Clear();

            if (Categories.Count != 0)
            {
                Categories.Clear();
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

    //Add search -> clear stack

    [RelayCommand]
    async Task Tap(List<Category> list) {
        if (list == null)
        {
            await Shell.Current.GoToAsync(nameof(ItemCatalogPage));
            //TODO: Get http request of passed items that belong to the collection
        }
        else {
            _pastCategories.Push(Categories);
            ObservableCollection<Category> collection = new ObservableCollection<Category>(list);
            UpdateCategory(collection);
        } 
    }

    private void UpdateCategory(ObservableCollection<Category> newCategory) {
        Categories.Clear();
        foreach (var category in newCategory) { 
            Categories.Add(category);
        }
    
    }

    [RelayCommand]
    static async void Back(object obj)
    {

        if (_pastCategories.Count == 0)
        {
            await Shell.Current.GoToAsync("..");
        }
        else {
            // TODO: Get Back to stack
            ObservableCollection<Category> pastCategory = (ObservableCollection<Category>)_pastCategories.Pop();
            UpdateCategory(pastCategory);
        }



    }

    //Refresh

}
