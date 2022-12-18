using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.Services;

public class ItemFilterService
{
    private ObservableCollection<Item> FetchedItems { get; set; } = new();

    public void FetchItems(ObservableCollection<Item> items)
    {
        FetchedItems = items;
    }
    
    internal ObservableCollection<Item> FilterAlphaAsc()
    {
        var searchResults = from I in FetchedItems
            orderby I.NameLT
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }

    internal ObservableCollection<Item> FilterAlphaDesc()
    {
        var searchResults = from I in FetchedItems
            orderby I.NameLT descending 
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }

    internal ObservableCollection<Item> FilterPriceAsc()
    {
        var searchResults = from I in FetchedItems
            orderby I.Price
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }

    internal ObservableCollection<Item> FilterPriceDesc()
    {
        var searchResults = from I in FetchedItems
            orderby I.Price descending 
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }

    internal ObservableCollection<Item> FilterUnitPriceAsc()
    {
        var searchResults = from I in FetchedItems
            orderby I.PricePerUnitOfMeasure
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }
    internal ObservableCollection<Item> FilterUnitPriceDesc()
    {
        var searchResults = from I in FetchedItems
            orderby I.PricePerUnitOfMeasure descending
            select I;
        return new ObservableCollection<Item>(searchResults.ToList());
    }

}

