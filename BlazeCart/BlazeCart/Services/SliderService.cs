using System.Collections.ObjectModel;
using BlazeCart.Models;

namespace BlazeCart.Services;


public  class SliderService
{
    private ObservableCollection<Item> FetchedItems { get; set; } = new();

    public double GetMaximum(ObservableCollection<Item> searchResults)
    {
        double max = searchResults.Max(I => I.Price);
        return max;
    }

    public double GetMinimum(ObservableCollection<Item> searchResults)
    {
        double min = searchResults.Min(I => I.Price);
        return min;
    }

    public void FetchItems(ObservableCollection<Item> items)
    {
        FetchedItems = items;
    }

    internal ObservableCollection<Item> GetSearchResults(double min, double max)
    {
        var searchResults = from I in FetchedItems
            where I.Price >= min && I.Price <= max
            select I;

        ObservableCollection<Item> itemSearchResult = new ObservableCollection<Item>(searchResults.ToList());
        return itemSearchResult;
    }

}

