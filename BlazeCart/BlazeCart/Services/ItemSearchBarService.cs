using System.Collections;
using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.Services;

public class ItemSearchBarService
{
    private ObservableCollection<Item> FetchedItems { get; set; } = new();

    public ItemSearchBarService(ObservableCollection<Item> Items)
    {
        FetchedItems = Items;
    }


    internal ObservableCollection<Item> GetSearchResults(string text)
    {
        var searchResults = from I in FetchedItems
            where I.Name.Contains(text)
            select I;

        foreach( Item r in searchResults) {
            Console.WriteLine(r.Name);
        }

        //Var search results to object
        ObservableCollection<Item> itemSearchResult = new ObservableCollection<Item>(searchResults.ToList());
        return itemSearchResult;
    }

}