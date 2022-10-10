using BlazeCart.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazeCart.Services;
using System.Diagnostics;
using Debug = System.Diagnostics.Debug;

//Inserting service


namespace BlazeCart.ViewModels;


public class ItemsViewModel : BaseViewModel
{
    //A collection that notifies when it is changed.
    public ObservableCollection<Item> Items { get; } = new();
    public Command GetItemsCommand { get;  }
    
    ItemService _itemService;

    //Dependency injection
    public ItemsViewModel() {
        //Paremtrize the parameters?
        name = "Item finder";
        ItemService _itemService = new ItemService();
        this._itemService = _itemService;
        GetItemsCommand = new Command(async () => await GetItemsAsync());
    }



    async Task GetItemsAsync() {

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
            if (Items.Count != 0) {
                Items.Clear();
            }

            //Adds items to the local collection
            foreach( var item in items)
                Items.Add(item);

        }

        catch (Exception ex)
        {
            //Logs error
            Debug.WriteLine($"Unable to get items: {  ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }

        finally {
            isBusy = false;
        }

        
    
    
    }
    

}
