using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.Services;

public class ItemSearchBarService
{
    private ObservableCollection<Item> FetchedItems { get; set; } = new();

    public void FetchItems(ObservableCollection<Item> items)
    {
        FetchedItems = items;
    }

    internal ObservableCollection<Item> GetSearchResults(string text)
    {
        var searchResults = from I in FetchedItems
            where I.NameLT.Contains(text)
            select I;

        ObservableCollection<Item> itemSearchResult = new ObservableCollection<Item>(searchResults.ToList());
        return itemSearchResult;
    }

}