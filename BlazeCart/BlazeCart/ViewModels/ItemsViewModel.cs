using BlazeCart.Models;
using BlazeCart.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;

//Inserting service
namespace BlazeCart.ViewModels;


public partial class ItemsViewModel : BaseViewModel
{
    //A collection that notifies when it is changed.
    public ObservableCollection<Item> Items { get; set; } = new();

    public ObservableCollection<Item> CartItems { get; set; } = new();

    public Item SelectedItem { get; set; }

    private Cart cart = new Cart();

    ItemService _itemService = new();
    CartService _cartService = new();

    public ItemsViewModel()
    {
        Items = new ObservableCollection<Item>();
        GetItemsAsync();
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
            //Loading items from service
            var items = await _itemService.GetItems();

            //Clears the local collection
            if (Items.Count != 0)
            {
                Items.Clear();
            }

            //Adds items to the local collection
            foreach (var item in items)
                Items.Add(item);
        }

        catch (Exception ex)
        {
            //Logs error
            Debug.WriteLine($"Unable to get items: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }

        finally
        {
            isBusy = false;
        }

    }

    [RelayCommand]
    async void Cart(Item item)
    {
        this.SelectedItem = item;
        this.CartItems.Add(SelectedItem);
        //await _cartService.ItemsToCart( this.CartItems, "Vau pavyko"); //error on this method
        await Application.Current.MainPage.DisplayAlert("Įdėta į krepšelį!", "Prekė sėkmingai įdėta į krepšelį!", "OK");

    }

}
