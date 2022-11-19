
using System.Collections.ObjectModel;
using System.Diagnostics;
using BlazeCart.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using BlazeCart.Services;

namespace BlazeCart.ViewModels;

public class CategoryViewModel : BaseViewModel
{
    public ObservableCollection<Category> Categories { get; set; } = new();
    private readonly CategoryService _categoryService;

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
}

